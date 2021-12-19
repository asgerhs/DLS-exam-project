import {useState, useEffect} from 'react'
import Nav from '../Hooks/Nav';
import { Table, Tag, Space } from 'antd';

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
            <h1>Student Page</h1>
            {/* <AllStudentsTable data={data} /> */}
            <Table dataSource={data} columns={columns}/>
        </div>
    )
}

const columns = [
    {
      title: 'Name',
      dataIndex: 'name',
      key: 'name',
      render: text => <a>{text}</a>,
    },
    {
      title: 'Email',
      dataIndex: 'email',
      key: 'email',
    },
    {
      title: 'Age',
      dataIndex: 'age',
      key: 'age',
    },
    {
      title: 'Gender',
      dataIndex: 'gender',
      key: 'gender',
    },
    {
      title: 'Action',
      key: 'action',
      render: (text, record) => (
        <Space size="middle">
          <a>Invite {record.name}</a>
          <a>Delete</a>
        </Space>
      ),
    },
  ];

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