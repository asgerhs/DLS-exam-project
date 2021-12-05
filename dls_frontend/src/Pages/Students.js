import {useState, useEffect} from 'react'
import Nav from '../Hooks/Nav';

function Students() {
    const [data, setData] = useState([]);
    const [data2, setData2] = useState();


    useEffect(() => {
        fetch('http://localhost:8000/students/all')
            .then(response => response.json())
            .then(dat => setData(dat))
            .then(console.log(data));
    }, []);

    return (
        
        <div>
            <Nav displayState={"block"}/>
            <h1>Student Page</h1>
            <AllStudentsTable data={data} />
        </div>
    )
}

function AllStudentsTable({data}) {
    return (
        <table className="table">
        <thead className="thead-dark">
        <tr>
          <th scope="col">Name</th>
          <th scope="col">Email</th>
          <th scope="col">Age</th>
          <th scope="col">Gender</th>
        </tr>
      </thead>
      <tbody>
          {data.map((student, index) => (
              <tr key={index}>
                  <td>{student.name}</td>
                  <td>{student.email}</td>
                  <td>{student.age}</td>
                  <td>{student.gender}</td>
              </tr>
          ))}
      </tbody>
      </table>
    )
}

export default Students;