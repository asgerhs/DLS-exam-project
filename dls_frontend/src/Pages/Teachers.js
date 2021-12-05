import {useState, useEffect} from 'react'
import Nav from '../Hooks/Nav';

function Teachers() {
    const [data, setData] = useState([]);
    const [data2, setData2] = useState();


    useEffect(() => {
        fetch('http://localhost:8000/teachers/all')
            .then(response => response.json())
            .then(dat => setData(dat))
            .then(console.log(data));
    }, []);

    return (
        
        <div>
            <Nav displayState={"block"}/>
            <h1>Teacher Page</h1>
            <AllTeachersTable data={data} />
        </div>
    )
}

function AllTeachersTable({data}) {
    return (
        <table className="table">
        <thead className="thead-dark">
        <tr>
          <th scope="col">Name</th>
          <th scope="col">Email</th>
        </tr>
      </thead>
      <tbody>
          {data.map((teacher, index) => (
              <tr key={index}>
                  <td>{teacher.name}</td>
                  <td>{teacher.email}</td>
              </tr>
          ))}
      </tbody>
      </table>
    )
}

export default Teachers;