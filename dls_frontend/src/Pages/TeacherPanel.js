import { useState, useEffect } from 'react'

function TeacherPanel({ isTeacher, username, error }) {
    const [code, setCode] = useState();

    const setNewCode = (newCode) => {
        setCode(newCode);
    }
    return (
        <div>
            <h1>Response - {code}</h1>
            <CreateLecture setNewCode={setNewCode} username={username}/>
        </div>
    )
}

function CreateLecture({ setNewCode, username, error }) {
    const [code, setCode] = useState();

    const submitHandler = async (e) => {
        e.preventDefault();
        async function fetchCode() {
            const requestOptions = {
                method: 'POST',
                headers: { 'Content-Type': 'text/plain' },
            };
            const response = await fetch('http://localhost:8000/lectures/addRegistrationCode/' + code.code, requestOptions)
            const resData = await response.text()
            console.log(resData)
            setNewCode(resData)
        }

        fetchCode();
    }

    const onChange = (evt) => {
        setCode({ ...code, [evt.target.id]: evt.target.value });
    }

    return (
        <div className="AppFirst">
            <form onSubmit={submitHandler} onChange={onChange}>
                <div className="form-inner">
                    <h2>Enter ID of lecture to get a code</h2>
                    <div className="form-group">
                        <h2>{setNewCode}</h2>
                        <label htmlFor="code">Code:</label>
                        <input type="username" name="code" id="code" />
                    </div>
                    <input type="submit" value="Register" />
                </div>
            </form>
        </div>
    )
}

export default TeacherPanel;
