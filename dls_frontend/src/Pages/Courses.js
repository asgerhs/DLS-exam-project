import {useState, useEffect} from 'react'
import Nav from '../Hooks/Nav';
import { Table, Modal, Button} from 'antd';
import { DownOutlined } from '@ant-design/icons';

function Courses() {
    const [data, setData] = useState([]);


  useEffect(() => {
    fetch('http://localhost:8000/courses/all')
        .then(response => response.json())
        .then(dat => setData(dat))
        .then(console.log(data));
  }, []);


    // useEffect(() => {
    //   async function com()  {
    //     const res = await fetch('http://localhost:8000/courses/all')
    //     const json = await res.json()
    //     setData(json)
    //     console.log(json)
    //   }
    //   com()
    // }, []);

      

    return (
        <div>
            <h1>Courses Page</h1>
            <Table dataSource={data} columns={columns} rowKey='name'/>
        </div>
    )
}

function DataModal(id) {
  const [visible, setVisible] = useState(false);
  const [confirmLoading, setConfirmLoading] = useState(false);
  const [modalText, setModalText] = useState([]);

  const showModal = (e) => {
    e.preventDefault();
    async function fetchLectures () {
      const empty = []
      const requestOptions = {
          method: 'GET',
          headers: { 'Content-Type': 'text/plain'},
      };
      const response = await fetch('http://localhost:8000/lectures/getLecturesByCourse/' + id, requestOptions)
      const resData = await response.json();
      setModalText(resData)
    }
    fetchLectures();
    setVisible(true);
  };

  const handleOk = () => {
    setConfirmLoading(true);
    setVisible(false);
    setConfirmLoading(false);
  };

  const handleCancel = () => {
    console.log('Clicked cancel button');
    setVisible(false);
  };

  const columns = [
    {
      title: 'ID',
      dataIndex: 'id',
      key: 'id'
    },
    {
      title: 'Name',
      dataIndex: 'name',
      key: 'name'
    },
    {
      title: 'Date',
      dataIndex: 'date',
      key: 'date'
    },
    {
      title: 'Attendance',
      dataIndex: 'date',
      key: 'date'
    },
    
  ];


  return (
    <>
      <Button type="primary" onClick={showModal}>
        Lectures
      </Button>
      <Modal
        title="List of lectures"
        visible={visible}
        onOk={handleOk}
        confirmLoading={confirmLoading}
        onCancel={handleCancel}
      >
        <Table dataSource={modalText} columns={columns} rowKey='id'/>
      </Modal>
    </>
  );
}
//#endregion

const columns = [
    {
      title: 'ID',
      dataIndex: 'id',
      key: 'id'
    },
    {
      title: 'Name',
      dataIndex: 'name',
      key: 'name',
    },
    {
      title: 'Students in course',
      dataIndex: 'name',
      key: 'name',
    },
    
    {
      title: 'Action',
      key: 'action',
      render: (text) => (
        DataModal(text.id)
      ),
    },
  ];




export default Courses;