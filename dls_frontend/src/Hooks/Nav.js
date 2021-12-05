import {useState} from 'react'
import { NavLink, Link, Navigate, useNavigate} from 'react-router-dom';
import App from '../App';



function Nav({someUser, displayState}) {
    
    const [loggedIn, setLoggedIn] = useState(true);
    const [show, setShow] = useState({ display: displayState });
    const [path, setPath] = useState("");
    const [userName, setUserName] = useState("");

    
    const Logout = () => {
        console.log("Logout");
        setLoggedIn(false);
        setShow({ display: "none" });
    }
    

    return (
        <div style={show}>
            <nav>
                <h3><span>CPH Business</span></h3>
                <ul className="nav-links">
                    <li>Students</li>
                    <li>Courses</li>
                    {/* <button onClick={() => navigate('students')}>Stusdents</button> */}
                    
                </ul>
                <button onClick={Logout}>Logout</button>
            </nav>
        </div>
    )
}


export default Nav
