import {useState, useEffect} from 'react'
import Nav from '../Hooks/Nav';
import { UseGeoLocation } from '../Hooks/UseGeoLocation';

function DailyCode({isTeacher, error}) {
    const [code, setCode] = useState();
    const [allow, setAllow] = useState(false);
    const [data, setData] = useState([]);
    const [data2, setData2] = useState();

    const setNewCode = (newCode) => {
        setCode(newCode);
    }

    const setNewAllow = (newAllow) => {
        setAllow(newAllow);
    }

    return (
        <div>
            <h1>Code of the day - {code}</h1>
            <CodeForm setNewCode={setNewCode} setNewAllow={setNewAllow} />
        </div>
    )
}


function CodeForm({setNewCode}) {
    const [loc, setLoc] = useState(true);
    const [code, setCode] = useState();

    // Geolocation check
    const locationOfUser = UseGeoLocation();
    const locationOfSchool = {latitude: "55.77068804647255", longitude: "12.511908027327893"};
    const geolib = require('geolib');

    const distance = geolib.getDistance(locationOfUser.coordinates, locationOfSchool, 1);
    const locationWithinRadius = geolib.isPointWithinRadius(locationOfUser.coordinates, locationOfSchool, 100)

    const submitHandler = async(e) => {
        e.preventDefault();

        async function fetchCode () {
            const requestOptions = {
                method: 'POST',
                headers: { 'Content-Type': 'text/plain'},
            };
            const response = await fetch('http://localhost:8000/lectures/addRegistrationCode/1', requestOptions)
            const resData = await response.text()
            setNewCode(resData)
            setLoc(locationWithinRadius);
        }
        
        fetchCode();
    }
    
    const onChange = (evt) => {
        setCode({ ...code, [evt.target.id]: evt.target.value });
    }

    return (
        <div className="AppFirst">
        <form onSubmit={submitHandler} onChange={onChange}>
            <div className="form-inner">
                <h2>Enter code to register attendance</h2>
                {loc ? "" : <div className="error">Not in radius of school</div>}
                <div className="form-group">
                    <label htmlFor="code">Code:</label>
                    <input type="username" name="code" id="code"/>
                </div>
                <input type="submit" value="Register" />
            </div>
        </form>
    </div>
    )
}

  

export default DailyCode;