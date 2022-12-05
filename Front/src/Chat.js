import React, { useState, useEffect, useRef } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';
import axios, { AxiosHeaders } from 'axios'

import ChatWindow from './ChatWindow';
import ChatInput from './ChatInput';

const Chat = () => {
    const [ connection, setConnection ] = useState(null);
    const [ chat, setChat ] = useState([]);
    const [ token, setToken ] = useState("");
    const latestChat = useRef(null);

    latestChat.current = chat;

    function NewConnection(){

        const newConnection = new HubConnectionBuilder()
            .withUrl('http://localhost:5001/chatRoom')
            .withAutomaticReconnect()
            .build();

        setConnection(newConnection);
    }

    useEffect(() => {
        NewConnection();
    }, []);

    useEffect(() => {
        if (connection) {
            connection.start()
                .then(async result => {
                    console.log('Connected!',result);

    
                    connection.on('ReceiveMessage', message => {

                        console.log('received',message)
                        const updatedChat = [...latestChat.current];
                        updatedChat.push(message);
                    
                        setChat(updatedChat);
                    });
                })
                .catch(e => console.log('Connection failed: ', e));
        }
        else
        NewConnection();
    }, [connection]);

    const sendMessage = async (message, chatRoomId) => {
        const chatMessage = {
            ChatRoomId : chatRoomId,
            message: message
        };

        axios.post('http://localhost:5001/api/ChatRoom/send-message', chatMessage, {
            headers: {
            'content-type': 'text/json',
            'Access-Control-Allow-Origin': '*',
            'Authorization': `Bearer ${token}`
            }
        })
        .then(response => {})
        .catch(error => {
            console.error('There was an error!', error);
            alert(error.response.data.value.data[0].message);
        });
        
    }

    const InputToken = (BearerToken) => {
        setToken(BearerToken);
    }

    const inputChat = (chatMessages) => {
        console.log(chatMessages)
        setChat(chatMessages);
    }
    return (
        <div>
            <ChatInput sendMessage={sendMessage} setToken={InputToken} inputChat={inputChat}/>
            <hr />
            <ChatWindow chat={chat}/>
        </div>
    );
};

export default Chat;