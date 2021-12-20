import {useState, useEffect} from 'react'
import Nav from '../Hooks/Nav';
import { Table, Tag, Space } from 'antd';

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
            <h1>Teacher Page</h1>
            <Table dataSource={data} columns={columns} />
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