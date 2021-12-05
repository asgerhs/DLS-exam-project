import {useState, useEffect} from 'react'
import Nav from '../Hooks/Nav';
import { Table, Badge, Menu, Dropdown, Space } from 'antd';
import { DownOutlined } from '@ant-design/icons';

function Courses() {
    const [data, setData] = useState([]);
    const [data2, setData2] = useState([]);


    useEffect(() => {
        fetch('http://localhost:8000/courses/all')
            .then(response => response.json())
            .then(dat => setData(dat))
            .then(console.log(data));

        const coursess = []
        data.forEach(element => {
            fetch('http://localhost:8000/courses/get/' + element.id)
            .then(response => response.json())
            .then(dat => coursess.push({
                id: dat.teacher.id,
                teacher: dat.teacher.name
            }));
        });
        setData2(coursess);
        console.log(data2);
    }, []);

    return (
        
        <div>
            <Nav displayState={"block"}/>
            <h1>Courses Page</h1>
            {/* <AllStudentsTable data={data} /> */}
            <NestedTable courseData={data, data2}/>
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


const menu = (
    <Menu>
      <Menu.Item>Action 1</Menu.Item>
      <Menu.Item>Action 2</Menu.Item>
    </Menu>
  );
  
  function NestedTable({courseData, singleData}) {
    const expandedRowRender = () => {
      const columns = [
        { title: 'ID', dataIndex: 'teacherId', key: 'teacherId' },
        { title: 'Teacher', dataIndex: 'teacher', key: 'teacher' },
        {
          title: 'Status',
          key: 'state',
          render: () => (
            <span>
              <Badge status="success" />
              Finished
            </span>
          ),
        },
        { title: 'Upgrade Status', dataIndex: 'upgradeNum', key: 'upgradeNum' },
        {
          title: 'Action',
          dataIndex: 'operation',
          key: 'operation',
          render: () => (
            <Space size="middle">
              <a>Pause</a>
              <a>Stop</a>
              <Dropdown overlay={menu}>
                <a>
                  More <DownOutlined />
                </a>
              </Dropdown>
            </Space>
          ),
        },
      ];
  
      
      return <Table columns={columns} dataSource={[]} pagination={false} />;
    };
  
    const columns = [
      { title: 'Course ID', dataIndex: 'id', key: 'id' },
      { title: 'Name', dataIndex: 'name', key: 'name' },
      { title: 'Action', key: 'operation', render: () => <a>Publish</a> },
    ];
  
    
    return (
        <Table
          className="components-table-demo-nested"
          columns={columns}
          expandable={{ expandedRowRender }}
          dataSource={courseData}
        />
      );
    }

export default Courses;