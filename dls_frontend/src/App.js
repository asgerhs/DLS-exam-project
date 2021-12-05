import {useState} from 'react';
import LoginForm from './Hooks/LoginForm';
import { UseGeoLocation } from './Hooks/UseGeoLocation';
import Nav from './Hooks/Nav';
import WelcomeStudent from './Pages/WelcomeStudent';
import WelcomeTeacher from './Pages/WelcomeTeacher';
import {BrowserRouter as Router, Routes, Route, useNavigate, Navigate, Link} from 'react-router-dom';
import Students from './Pages/Students';
import Courses from './Pages/Courses';
import Teachers from './Pages/Teachers';
import Statistics from './Pages/Statistics';



function App() {
  const adminUser = {
    username: "admin",
    password: "admin123",
    location: "",

  }

  const [user, setUser] = useState({name: "", username: "", location: "", role: ""});
  const [loggedIn, setLoggedIn] = useState(false);
  const [disp, setDisplay] = useState("none");
  const [error, setError] = useState("");

  // const locationOfUser = UseGeoLocation();
  // const locationOfSchool = {latitude: "55.77068804647255", longitude: "12.511908027327893"};
  // const geolib = require('geolib');

  // const distance = geolib.getDistance(locationOfUser.coordinates, locationOfSchool, 1);
  // const locationWithinRadius = geolib.isPointWithinRadius(locationOfUser.coordinates, locationOfSchool, 100)
  

  const Login = details => {
    console.log(details);

    if (details !== null) {
      console.log("Logged in");
      console.log("Some details: " + details);
      // setUser({
      //   name: details.name,
      //   username: details.username,
      //   role: "Teacher"
      // });
      setDisplay("block")
      setLoggedIn(true);
   
      
      details.isTeacher 
      ? navigate("/welcometeacher")
      : navigate("/welcomestudent")

    } else {
      setError("Details don't match");
      console.log("Details don't match");
    }
    
    

  }
  
  let navigate = useNavigate();
  

  return (
    <div >
          <Nav displayState={disp}/>
          <Routes>
            <Route exact path ="/" element={<LoginForm Login={Login} error={error}/>} />
            <Route path="/welcometeacher" element={<WelcomeTeacher />} />
            <Route path="/welcomestudent" element={<WelcomeStudent />} />
            <Route path="/students" element={<Students/>} />
            <Route path="/teachers" element={<Teachers/>} />
            <Route path="/courses" element={<Courses />} />
            <Route path="/statistics" element={<Statistics/>} />
          </Routes>
    </div>
  );
}

function Welcome() {
  return (
    <div className="d-flex justify-content-center align-items-center link">
      <a href="https://github.com/asgerhs/CA-3/blob/master/README.md">Press me for quick start guide!!</a>
    </div>
  )
}




const Footer = () => {
  return (
    <footer>
        <span> © Copyright 2019 -  William Huusfeldt. </span>
    </footer>
  )
}
// require('react-dom');
// window.React2 = require('react');
// console.log(window.React1 === window.React2);

export default App;
