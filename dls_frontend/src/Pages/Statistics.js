import {useState, useEffect} from 'react'
import Nav from '../Hooks/Nav';

function Statistics() {
    const [data, setData] = useState([]);
    const [data2, setData2] = useState();


    useEffect(() => {
        fetch('http://localhost:8000/statistics/studentsAttendedLecture/1')
            .then(response => response.json())
            .then(dat => setData(dat))
            .then(console.log(data));
    }, []);

    return (
        
        <div>
            <h1>Statistics Page</h1>
            <CodeForm setNewCode={data} />
            <StatisticsTable data={data} />
        </div>
    )
}

function StatisticsTable({data}) {
    return (
        <table className="table">
        <thead className="thead-dark">
        <tr>
          <th scope="col">Name</th>
        </tr>
      </thead>
      <tbody>
        <tr >
            <td>{data}</td>
        </tr>
      </tbody>
      </table>
    )
}

function CodeForm({setNewCode}) {
    const [loc, setLoc] = useState(true);
    const [code, setCode] = useState();


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
        
        <form onSubmit={submitHandler} onChange={onChange}>
            <div className="form-inner-right">
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

export default Statistics;