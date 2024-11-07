import React, { useEffect, useState, useRef } from "react";
import CommentForm from "../Comment/CommentForm/CommentForm.tsx";
import CommentsList from "../Comment/CommentsList/CommentsList.tsx";
import { useGetTopLayerCommentsQuery } from "../../API/comments.ts";
import { Comment } from "../../Types/comments.ts";
const Websocket: React.FC = () => {
	const [modal, setModal] = useState<boolean>(false);
	const [message, setMessage] = useState<string>("");
	const [messages, setMessages] = useState<string[]>([]);
	const [socket, setSocket] = useState<WebSocket | null>(null);
	const [comments, setComments] = useState<Comment[]>([]);
	const { data: topLayerComments, refetch } = useGetTopLayerCommentsQuery();
	function addComments(commentsArr: Comment[]) {
		setComments((prevstate) => {
			let temp: Comment[] = [];
			commentsArr.forEach((element) => {
				if (!prevstate.includes(element)) {
					temp.push(element);
				}
			});
			console.log(prevstate, temp);
			return [...prevstate, ...temp];
		});
	}
	useEffect(() => {
		console.log(topLayerComments);
		if (topLayerComments && topLayerComments.comments) {
			addComments(topLayerComments.comments);
		}
	}, [topLayerComments]);
	useEffect(() => {
		const ws = new WebSocket("ws://localhost:5243/api/comments/ws");

		ws.onopen = () => {
			console.log("Connected to WebSocket");
		};

		ws.onmessage = (event) => {
			setMessages((prevMessages) => [...prevMessages, event.data]);
			console.log(event.data);
			let commentTmp = JSON.parse(event.data);
			let comment: Comment = {
				author: {
					email: commentTmp.Author.Email,
					username: commentTmp.Author.Username,
					homepage: commentTmp.Author.Homepage,
				},
				content: commentTmp.Content,
				fileUrl: commentTmp.FileURL,
				createdAt: commentTmp.CreatedAt,
				id: commentTmp.Id,
				childrenCount: commentTmp.ChildrenCount,
				parentId: commentTmp.ParentId,
			};
			console.log(comment);
			addComments([comment]);
		};

		ws.onerror = (error) => {
			console.error("WebSocket error:", error);
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
		<div style={{ width: "60vw", margin: "auto" }}>
			<button onClick={() => setModal(true)}>Write a comment</button>
			<CommentsList comments={comments} />
			{modal && <CommentForm parentId={null} setModal={setModal} />}
		</div>
	);
};

export default Websocket;
