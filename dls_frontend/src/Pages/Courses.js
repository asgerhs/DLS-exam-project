import {useState, useEffect} from 'react'
import Nav from '../Hooks/Nav';
import { Table, Badge, Menu, Dropdown, Space } from 'antd';
import { DownOutlined } from '@ant-design/icons';

function Courses() {
    const [data, setData] = useState([]);
    const [data2, setData2] = useState([]);


    useEffect(() => {
      async function com()  {
        const res = await fetch('http://localhost:8000/courses/all')
        const json = await res.json()
        setData(json)
        console.log(json)
      }
      com()
        const coursess = []
        // data.forEach(element => {
        //     fetch('http://localhost:8000/courses/get/' + element.id)
        //     .then(response => response.json())
        //     .then(dat => coursess.push({
        //         id: dat.teacher.id,
        //         teacher: dat.teacher.name
        //     }));
        // });
        // setData2(coursess);
        console.log(data);
    }, []);

    return (
        
        <div>
            <h1>Courses Page</h1>
            {/* <AllStudentsTable data={data} /> */}
            <NestedTable courseData={data}/>
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
  
function NestedTable({courseData}) {
  //#region nested table
  const expandedRowRender = (dat) => {
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

    const data = [];
    for (let i = 0; i < 3; ++i) {
      data.push({
        key: i,
        date: '2014-12-24 23:12:00',
        name: 'This is production name',
        upgradeNum: 'Upgraded: 56',
      });
    }

    
    return <Table columns={columns} dataSource={data} pagination={false} />;
  };
  //#endregion
  const columns = [
    { title: 'Course ID', dataIndex: 'id', key: 'id' },
    { title: 'Name', dataIndex: 'name', key: 'name' },
    { title: 'Teacher', dataIndex: 'teacher', key: 'teacher' },
    { title: 'students', dataIndex: 'students', key: 'students' },
    { title: 'teacherId', dataIndex: 'teacherId', key: 'teacherId' },
    { title: 'studentIds', dataIndex: 'studentIds', key: 'studentIds' },
  ];

  const data = [];
  for (let i = 0; i < 3; ++i) {
    data.push({
      key: i,
      name: 'Screem',
      platform: 'iOS',
      version: '10.3.4.5654',
      upgradeNum: 500,
      creator: 'Jack',
      createdAt: '2014-12-24 23:12:00',
    });
  }

  
  return (
      <Table
        rowKey={e=> e._id}
        className="components-table-demo-nested"
        columns={columns}
        expandable={{ expandedRowRender }}
        dataSource={courseData}
      />
    );
}

export default Courses;