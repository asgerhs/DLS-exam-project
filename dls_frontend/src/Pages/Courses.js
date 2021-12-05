import {useState, useEffect} from 'react'
import Nav from '../Hooks/Nav';

function Courses() {
    const [data, setData] = useState([]);
    const [data2, setData2] = useState();


    useEffect(() => {
        fetch('http://localhost:8000/courses/all')
            .then(response => response.json())
            .then(dat => setData(dat))
            .then(console.log(data));
    }, []);

    return (
        
        <div>
            <Nav displayState={"block"}/>
            <h1>Courses Page</h1>
            <AllStudentsTable data={data} />
        </div>
    )
}

function AllStudentsTable({data}) {
    return (
        <table className="table">
        <thead className="thead-dark">
        <tr>
          <th scope="col">ID</th>
          <th scope="col">Name</th>
        </tr>
      </thead>
      <tbody>
          {data.map((course, index) => (
              <tr key={index}>
                  <td>{course.id}</td>
                  <td>{course.name}</td>
              </tr>
          ))}
      </tbody>
      </table>
    )
}

export default Courses;