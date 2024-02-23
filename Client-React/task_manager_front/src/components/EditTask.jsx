import { useState } from 'react';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Modal from 'react-bootstrap/Modal';
import axios from "axios";
export const BASE_URL = "http://localhost:5030/";

function EditTask({ title, description, status, id, setTasks }) {
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    const [task, setTask] = useState({
        title: title,
        description: description,
        status_id: 2,
        status: status
    });

    const editTask = (e) => {
        e.preventDefault();
        const data = task;

        axios
            .put(BASE_URL + "api/Task/UpdateTask/" + id, data)
            .then((res) => {
                console.log(res);
                setTasks((prevTasks) =>
                    prevTasks.map((item) =>
                        item.id === id ? { ...item, ...data } : item
                    )
                );
            })
            .catch((err) => console.log(err));

        handleClose();
    };


    return (
        <>
            <i className="fas fa-edit" onClick={handleShow}></i>

            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Edit task</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
                            <Form.Label>Update title</Form.Label>
                            <Form.Control
                                type="email"
                                placeholder="create API endpoints"
                                value={task.title}
                                onChange={(e) => setTask({ ...task, title: e.target.value })}
                                autoFocus
                            />
                        </Form.Group>
                        <Form.Group
                            className="mb-3"
                            controlId="exampleForm.ControlTextarea1"
                        >
                            <Form.Label>Update description</Form.Label>
                            <Form.Control as="textarea" rows={3} value={task.description}
                                onChange={(e) => setTask({ ...task, description: e.target.value })}
                            />
                        </Form.Group>
                        <Form.Select
                            aria-label="Default select example"
                            onChange={(e) => {
                                const temp = parseInt(e.target.value, 10);
                                const selectedStatus = temp === 1 ? "complete" : "incomplete";
                                setTask({ ...task, status_id: temp, status: selectedStatus });
                            }}
                        >
                            <option>Is the task complete?</option>
                            <option value="1">Complete</option>
                            <option value="2">Incomplete</option>
                        </Form.Select>

                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="outline-danger" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="outline-success" onClick={editTask}>
                        Save Changes
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    );
}

export default EditTask;