import React, { useEffect, useState } from "react";
import CommentForm from "../Comment/CommentForm/CommentForm.tsx";

const Websocket: React.FC = () => {
	const [message, setMessage] = useState<string>("");
	const [messages, setMessages] = useState<string[]>([]);
	const [socket, setSocket] = useState<WebSocket | null>(null);

	useEffect(() => {
		const ws = new WebSocket("wss://localhost:7085/api/comments/ws");

		ws.onopen = () => {
			console.log("Connected to WebSocket");
		};

		ws.onmessage = (event) => {
			setMessages((prevMessages) => [...prevMessages, event.data]);
		};

		ws.onclose = () => {
			console.log("Disconnected from WebSocket");
		};

		setSocket(ws);

		return () => {
			ws.close();
		};
	}, []);

	const sendMessage = () => {
		if (socket && socket.readyState === WebSocket.OPEN) {
			socket.send(message);
			setMessage("");
		}
	};

	return (
		<div>
			{/* <div>
				{messages.map((msg, index) => (
					<div key={index}>{msg}</div>
				))}
			</div> */}
			<CommentForm />
			{/* <input
				type="text"
				value={message}
				onChange={(e) => setMessage(e.target.value)}
				placeholder="Type a message"
			/>
			<button onClick={sendMessage}>Send</button> */}
		</div>
	);
};

export default Websocket;
