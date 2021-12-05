import {useState} from 'react';
import LoginForm from './Hooks/LoginForm';
import { UseGeoLocation } from './Hooks/UseGeoLocation';
import Nav from './Hooks/Nav';
import WelcomeStudent from './Pages/WelcomeStudent';
import WelcomeTeacher from './Pages/WelcomeTeacher';
import {BrowserRouter as Router, Routes, Route, useNavigate, Navigate} from 'react-router-dom';
import Students from './Pages/Students';



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
  const locationOfUser = UseGeoLocation();
  const locationOfSchool = {latitude: "55.77068804647255", longitude: "12.511908027327893"};
  const geolib = require('geolib');

  const distance = geolib.getDistance(locationOfUser.coordinates, locationOfSchool, 1);
  const locationWithinRadius = geolib.isPointWithinRadius(locationOfUser.coordinates, locationOfSchool, 100)
  

  const Login = details => {
    console.log(details);

    if (details.username === adminUser.username && details.password === adminUser.password) {
      console.log("Logged in");
      setUser({
        name: details.name,
        username: details.username,
        role: "Teacher"
      });
      setDisplay({disp: "block"})
      setLoggedIn(true);

    } else {
      setError("Details don't match");
      console.log("Details don't match");
    }
  }

  

  const Logout = () => {
    console.log("Logout");
    setError("");
    setUser({name: "", username: ""});
  }

  const Routing = () => {
    return (
      !loggedIn 
        ? <LoginForm Login={Login} error={error}/>
        : (user.role == "Teacher" 
          ? <Navigate to="/welcometeacher" />
          : <Navigate to="/welcomestudent" />
        ) 
            
    )
  
  }

  return (
    <div >
        <Router>
          <Routing />
          <Routes>
            <Route exact path ="/" element={<Welcome/>} />
            <Route path="/welcometeacher" element={<WelcomeTeacher teacherUser={user}/>} />
            <Route path="/welcomestudent" element={<WelcomeStudent studentUser={user}/>} />
            <Route path="/students" element={<Students/>} />
          </Routes>
        </Router>
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
require('react-dom');
window.React2 = require('react');
console.log(window.React1 === window.React2);

export default App;
