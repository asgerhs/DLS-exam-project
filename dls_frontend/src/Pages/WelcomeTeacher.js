import {useState} from 'react';
import { UseGeoLocation } from '../Hooks/UseGeoLocation';
import Nav from '../Hooks/Nav';
import { BrowserRouter as Router } from 'react-router-dom';


function WelcomeTeacher() {
  const adminUser = {
    username: "admin",
    password: "admin123",
    location: ""
  }

  // const [user, setUser] = useState({name: "", username: "", location: ""});
  // const [error, setError] = useState("");
  // const locationOfUser = UseGeoLocation();
  // const locationOfSchool = {latitude: "55.77068804647255", longitude: "12.511908027327893"};
  // const geolib = require('geolib');

  // const distance = geolib.getDistance(locationOfUser.coordinates, locationOfSchool, 1);
  // const locationWithinRadius = geolib.isPointWithinRadius(locationOfUser.coordinates, locationOfSchool, 100)

  // const Logout = () => {
  //   console.log("Logout");
  //   setError("");
  //   setUser({name: "", username: ""});
  // }


  return (
    <div>
        <Nav displayState={"block"}/>
        <h2>Welcome teacher, </h2>
        <div>
            {/* {locationOfUser.loaded
            ? (locationWithinRadius ? "Inside radius" : "Not inside radius")
            : "Location data not available yet"} */}
        </div>
        {/* <button className="welcome" onClick={Logout}>Logout</button> */}
    </div>
  );
}

export default WelcomeTeacher;
