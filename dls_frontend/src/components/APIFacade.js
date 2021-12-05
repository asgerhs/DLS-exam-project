const URL = 'http://localhost:8000/';

function handleHttpErrors(res) {
    if (!res.ok) {
      return Promise.reject({ status: res.status, fullError: res.json() });
    }
    return res.json();
  }
  
  function ApiFacade() {

  
    const makeOptions = (method, body) => {
      var opts = {
        method: method,
        headers: {
          'Content-type': 'application/json',
          mode: 'no-cors'
        },
      };
      if (body) {
        opts.body = JSON.stringify(body);
      }
      return opts;
    };
  
    // const login = (user, pass) => {
    //   const options = makeOptions('POST', true, { username: user, password: pass });
    //   return fetch(URL + '/api/login', options)
    //     .then(handleHttpErrors)
    //     .then(res => {
    //       setToken(res.token);
    //     });
    // };
  
    // const fetchUser = () => {
    //   const options = makeOptions('GET', true); //True add's the token
    //   return fetch(URL + '/api/info/user', options).then(handleHttpErrors);
    // };
  
    const fetchCourse = id => {
        return fetch('http://localhost:8000/courses/get/' + id, makeOptions('GET'));
      };
  
    
  
    return {
      fetchCourse
    };
  }
  
  export default ApiFacade();