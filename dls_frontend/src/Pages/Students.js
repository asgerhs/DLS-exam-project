import {useState, useEffect} from 'react'
import Nav from '../Hooks/Nav';
import { Table, Tag, Space, Modal, Button} from 'antd';

function Students() {
    const [data, setData] = useState([]);

    useEffect(() => {
        fetch('http://localhost:8000/students/all')
            .then(response => response.json())
            .then(dat => setData(dat))
            .then(console.log(data));
    }, []);

    return (
        <div>
            <h1>Student Page</h1>
            <Table dataSource={data} columns={columns} rowKey='name'/>
        </div>
    )
}

//#region DataModal
function DataModal(id) {
  const [visible, setVisible] = useState(false);
  const [confirmLoading, setConfirmLoading] = useState(false);
  const [modalText, setModalText] = useState({student: {}, courses: []});

  const showModal = (e) => {
    e.preventDefault();
    async function fetchStudent () {
        const empty = []
        const requestOptions = {
            method: 'GET',
            headers: { 'Content-Type': 'text/plain'},
        };
        const response = await fetch('http://localhost:8000/students/get/' + id, requestOptions)
        const resData = await response.json()
        
        for (let i = 0; i < resData['courses'].length; i++) {
          const attRes = await fetch(
            'http://localhost:8000/statistics/GetCourseAttendance/' + id + 
            '/' + resData['courses'][i]['id'], requestOptions)

          empty.push({id: resData['courses'][i]['id'], name: resData['courses'][i]['name'], attendedLectures: await attRes.text()})
        }
        
        setModalText({student: resData, courses:empty})

      }
    fetchStudent();
    setVisible(true);
  };

  const handleOk = () => {
    setModalText('The modal will be closed after two seconds');
    setConfirmLoading(true);
    setTimeout(() => {
      setVisible(false);
      setConfirmLoading(false);
    }, 750);
  };

  const handleCancel = () => {
    console.log('Clicked cancel button');
    setVisible(false);
  };

  const columns = [
    {
      title: 'Course ID',
      dataIndex: 'id',
      key: 'id'
    },
    {
      title: 'Name',
      dataIndex: 'name',
      key: 'name'
    },
    {
      title: 'Attended lectures',
      dataIndex: 'attendedLectures',
      key: 'attendedLectures'
    }
  ];


  return (
    <>
      <Button type="primary" onClick={showModal}>
        Courses
      </Button>
      <Modal
        title="List of courses student is attending"
        visible={visible}
        onOk={handleOk}
        confirmLoading={confirmLoading}
        onCancel={handleCancel}
      >
        <Table dataSource={modalText.courses} columns={columns} rowKey='id'/>
      </Modal>
    </>
  );
}
//#endregion


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
        DataModal(text.id)
      ),
    },
  ];


export default Students;