import {useState} from 'react';
import { UseGeoLocation } from '../Hooks/UseGeoLocation';
import Nav from '../Hooks/Nav';
import { Routes, Route } from 'react-router-dom';


function WelcomeStudent({studentUser}) {

  // const [user, setUser] = useState(studentUser);
  // const [error, setError] = useState("");
  // const locationOfUser = UseGeoLocation();
  // const locationOfSchool = {latitude: "55.77068804647255", longitude: "12.511908027327893"};
  // const geolib = require('geolib');

  // const distance = geolib.getDistance(locationOfUser.coordinates, locationOfSchool, 1);
  // const locationWithinRadius = geolib.isPointWithinRadius(locationOfUser.coordinates, locationOfSchool, 100)
  

  // const Logout = () => {
  //   console.log("Logout");
  //   setError("");
  //   setUser(studentUser.username = "", studentUser.password = "");
    
  // }


  return (
    <div>
      
    </div>
  );
}

require('react-dom');
window.React2 = require('react');
console.log(window.React1 === window.React2);

const Home = () => (
  <div>
    <h1>Home Page</h1>
  </div>
)

export default WelcomeStudent;
