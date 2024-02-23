import React from 'react';
import Table from 'react-bootstrap/Table';
import { useEffect, useState } from "react";
import axios from "axios";
import AddTask from './AddTask';
import DeleteTask from './DeleteTask';
import EditTask from './EditTask';
export const BASE_URL = "http://localhost:5030/";

export default function Home() {
  const [tasks, setTasks] = useState([]);
  useEffect(() => {
    axios
      .get(BASE_URL + "api/Task/getAllTasks/")
      .then((res) => {
        setTasks((prevTasks) => {
          console.log(prevTasks);
          return res.data; 
        });
      })
      .catch((err) => console.log(err));
  }, []);
  return (
    <div>
      Add new task by pressing plus icon   <br></br>
      <AddTask tasks={tasks} setTasks={setTasks}/>
      <Table striped bordered hover className='mt-3'>
        <thead>
          <tr>
            <th>Number</th>
            <th>Task title</th>
            <th>Description</th>
            <th>Modify</th>
          </tr>
        </thead>
        <tbody>
          {tasks?.map((task, index) => {
            return (
              <tr>
              <td>{index+1}</td>
              <td>{task.title}</td>
              <td>{task.description}</td>
              <td>{task.status || "incomplete"}</td>
              <td>  
                <EditTask tasks={tasks} setTasks={setTasks} title = {task.title} description={task.description} status = {task.status} id={task.id}/>
                <DeleteTask deleting={task.id} tasks={tasks} setTasks={setTasks}/>
              </td>
            </tr>
            )
          })}
        </tbody>
      </Table>
    </div>
  )
}
