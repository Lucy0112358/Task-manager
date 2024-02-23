import { useState, useEffect } from 'react';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import axios from "axios";
import Modal from 'react-bootstrap/Modal';
export const BASE_URL = "http://localhost:5030/";


function AddTask({ setTasks, tasks }) {
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    const [task, setTask] = useState({
        title: "",
        description: "",
        status_id: 2
    });
    const createTask = (e) => {
        e.preventDefault();
        const data = task;
        axios
            .post(BASE_URL + "api/Task/PostTask", data)
            .then((res) => {
                console.log(res);
                setTasks(prevTasks => [...prevTasks, res.data]);
            })
            .catch((err) => console.log(err));
        handleClose();
    }

    return (
        <>
            <svg
                xmlns="http://www.w3.org/2000/svg"
                width="26"
                height="26"
                fill="#5468ff"
                class="bi bi-plus-lg"
                viewBox="0 0 16 16"
                onClick={handleShow}
            >

                <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zM8.5 4.5a.5.5 0 0 0-1 0v3h-3a.5.5 0 0 0 0 1h3v3a.5.5 0 0 0 1 0v-3h3a.5.5 0 0 0 0-1h-3v-3z" />
            </svg>
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Add new task here!</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
                            <Form.Label>Title</Form.Label>
                            <Form.Control
                                type="email"
                                placeholder="create API endpoints"
                                onChange={(e) => {
                                    setTask({ ...task, title: e.target.value });
                                }}
                                autoFocus
                            />
                        </Form.Group>
                        <Form.Group
                            className="mb-3"
                            controlId="exampleForm.ControlTextarea1"
                        >
                            <Form.Label>Description</Form.Label>
                            <Form.Control as="textarea" rows={3}
                                onChange={(e) => {
                                    console.log(e.target.value);
                                    setTask({ ...task, description: e.target.value });
                                }} />
                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="outline-danger" onClick={handleClose}>
                        Cancel
                    </Button>
                    <Button variant="outline-success" onClick={createTask}>
                        Create task
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    );
}

export default AddTask;