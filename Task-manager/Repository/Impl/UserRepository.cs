﻿using Konscious.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Task_Management.Repository.Interfaces;

namespace Task_Management.Repository.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString = string.Empty;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        string IUserRepository.Register(string email, string password)
        {
            try
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("INSERT INTO Users (email, password) VALUES (@Email, @Password)", connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", hashedPassword);
                        command.ExecuteNonQuery();
                        return "user created successfully";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        string IUserRepository.SignIn(string email, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT Id, email, password FROM Users WHERE email = @Email", connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int userId = reader.GetInt32(0);
                            string userEmail = reader.GetString(1);
                            string hashedPassword = reader.GetString(2);
                            if (BCrypt.Net.BCrypt.Verify(password, hashedPassword))
                            {
                                return GetAccessToken(userEmail, userId);
                            }
                        }

                    }
                    return "Invalid email or password";
                }
            }
        }

        private string GetAccessToken(string email, int id)
        {
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email), "Email cannot be null");
            }

            string signingKey = _configuration["Jwt:SigningKey"];
            if (string.IsNullOrEmpty(signingKey))
            {
                throw new InvalidOperationException("Jwt:SigningKey configuration is missing or invalid");
            }


            byte[] keyBytes = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(keyBytes);
            }


            var securityKey = new SymmetricSecurityKey(keyBytes);

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                "issuer",
                "aud",
                new List<Claim>() {
            new Claim(JwtRegisteredClaimNames.NameId, id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim("role", "Admin")
                },
                expires: DateTime.UtcNow.AddDays(14),
                signingCredentials: credentials
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return accessToken;
        }


    }
}