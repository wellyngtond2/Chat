import React, { useState , useEffect} from 'react';
import Select from 'react-select';
import axios from 'axios'

const ChatInput = (props) => {
    const [user, setUser] = useState('');
    const [password, setPassword] = useState('');
    const [message, setMessage] = useState('');
    const [rooms , setRooms] = useState([]);
    const [isLogged , setIsLogged] = useState(false);
    const [token , setToken] = useState("");
    const [chatRoomId , setChatRoomId] = useState(-1);

    const onSubmitMessage = (e) => {
        e.preventDefault();

        const isUserProvided = user && user !== '';
        const isMessageProvided = message && message !== '';

        if (isUserProvided && isMessageProvided) {
            
            props.sendMessage(message, chatRoomId);
        } 
        else {
            alert('Please insert an user and a message.');
        }
    }
    
    const onLogin = async (e) => {
        const authenticateRequest = {
            Email : user,
            Password: password
        };
        
       await axios.post('http://localhost:5001/api/auth', authenticateRequest,{
            headers: {
            'content-type': 'text/json',
            'Access-Control-Allow-Origin': '*',
            }
        })
        .then(response => {
            if(response.data.data.token){
            setIsLogged(true);
            props.setToken(response.data.data.token)
            setToken(response.data.data.token)
            document.getElementById("loginArea").setAttribute("hidden","hidden");
            GetChatRoom();
            }
        })
        .catch(error => {
            console.error('There was an error!', error);
            alert(error.response.data.value.data[0].message);
        });
    }
    const onUserUpdate = (e) => {
        setUser(e.target.value);
    }

    const onMessageUpdate = (e) => {
        setMessage(e.target.value);
    }

    const onPasswordUpdate= (e)=> {
        setPassword(e.target.value);
    }

    const GetChatRoom = (e) =>{
        axios.get('http://localhost:5001/api/ChatRoom/all', {
            headers: {
            'content-type': 'text/json',
            'Access-Control-Allow-Origin': '*',
            }
        })
        .then(response => {
            var allRooms = response.data.map(x => ( { label: x.name, value : x.id}));
            setRooms(allRooms)
        })
        .catch(error => {
            console.error('There was an error!', error);
            alert(error.response.data.value.data[0].message);
        });
    }

    const GetChatRoomMessages = (roomId) =>{
        axios.get(`http://localhost:5001/api/ChatRoom/${roomId}/message`, {
            headers: {
            'content-type': 'text/json',
            'Access-Control-Allow-Origin': '*',
            'Authorization': `Bearer ${token}`
            }
        })
        .then(response => {
            var allChat = response.data.map(x => ( { user: x.membershipName, message : x.message}));
            
            props.inputChat(allChat)
        })
        .catch(error => {
            console.error('There was an error!', error);
            alert(error.response.data.value.data[0].message);
        });
    }

    const SetChatRoom=(e)=>{
        setChatRoomId(e.value);
        GetChatRoomMessages(e.value);
    }

    return (
        <>
        <div id="loginArea">
            <div>
            <label htmlFor="userId">User:</label>
            <br />
            <input 
                type="text"
                id="userId"
                name="userId" 
                value={user}
                onChange={onUserUpdate} />
            <br/><br/>
            <label htmlFor="Password">Password:</label>
            <br />
            <input 
                type="Password"
                id="Password"
                name="Password" 
                value={password}
                onChange={onPasswordUpdate} />
            <br/><br/>
            </div>
            <button onClick={onLogin}>Submit</button>
            <br/>
        </div>

        <div >
            <label>Choose the room</label>
            <Select onChange={SetChatRoom} options={rooms}></Select>            
            <label htmlFor="message">Message:</label>
            <br />
            <input 
                type="text"
                id="message"
                name="message" 
                value={message}
                onChange={onMessageUpdate} />
            <br/><br/>
            <button onClick={onSubmitMessage}>Send</button>
        </div>
        </>)
};

export default ChatInput;