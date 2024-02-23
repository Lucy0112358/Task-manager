import './App.css';
import Home from "./components/Home";
import AddTask from "./components/AddTask";
import DeleteTask from "./components/DeleteTask";
import EditTask from "./components/EditTask";
import { Routes, Route } from "react-router-dom";


function App() {
  return (
    <div className="App">
  <Routes>
  <Route path="/" element={<Home />} />
  <Route path="/edit-task" element={<EditTask />} />
  <Route path="/create-task" element={<AddTask />} />
  <Route path="/delete-task" element={<DeleteTask />} />  
  </Routes>
    </div>
  );
}

export default App;
