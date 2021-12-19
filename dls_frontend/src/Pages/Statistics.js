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

export default Statistics;