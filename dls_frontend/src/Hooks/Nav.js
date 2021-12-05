import {useState} from 'react'
import { NavLink, Link, Navigate, useNavigate} from 'react-router-dom';
import App from '../App';



function Nav({someUser, displayState}) {
    
    const [loggedIn, setLoggedIn] = useState(true);
    const [show, setShow] = useState(displayState);
    const [path, setPath] = useState("");
    const [userName, setUserName] = useState("");

    
    const Logout = () => {
        console.log("Logout");
        setLoggedIn(false);
        setShow("none");
    }

    

    const navigateTo = useNavigate();
    
    

    return (
        <div style={{display: displayState}} >
            <nav>
                <h3><span>CPH Business</span></h3>
                <ul className="nav-links">
                    <Link to="/courses">
                        <button type="button">Courses</button>
                    </Link>
                    <Link to="/teachers">
                        <button type="button">Teachers</button>
                    </Link>
                    <Link to="/students">
                        <button type="button">Students</button>
                    </Link>
                    <Link to="/statistics">
                        <button type="button">Statistics</button>
                    </Link>
                </ul>
                <Link to="/">
                    <button onClick={Logout}>Logout</button>
                </Link>
            </nav>
        </div>
    )
}

// require('react-dom');
// window.React2 = require('react');
// console.log(window.React1 === window.React2);


export default Nav
