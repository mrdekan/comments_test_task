import React, { useEffect, useState, Promise } from "react";
import CommentForm from "../Comment/CommentForm/CommentForm.tsx";
import CommentsList from "../Comment/CommentsList/CommentsList.tsx";
import {
	useGetTopLayerCommentsQuery,
	useLazyGetChildrenQuery,
} from "../../API/comments.ts";
import { Comment } from "../../Types/comments.ts";
import FileViewer from "../FileViewer/FileViewer.tsx";
const Websocket: React.FC = () => {
	const [page, setPage] = useState<number>(1);
	const [totalPages, setTotalPages] = useState<number>(0);
	const [modal, setModal] = useState<boolean>(false);
	const [fileToView, setFileToView] = useState<string | null>(null);
	const [openComments, setOpenComments] = useState<number[]>([]);
	const [responseId, setResponseId] = useState<number | null>(null);
	const [message, setMessage] = useState<string>("");
	const [messages, setMessages] = useState<string[]>([]);
	const [socket, setSocket] = useState<WebSocket | null>(null);
	const [comments, setComments] = useState<{ [key: string]: Comment[] }>({});
	const { data: topLayerComments, refetch } = useGetTopLayerCommentsQuery(page);
	const [getChildrenAsync] = useLazyGetChildrenQuery();
	function addComments(commentsArr: Comment[], newComment: boolean) {
		setComments((prevState) => {
			let updatedComments = { ...prevState };

			commentsArr.forEach((comment) => {
				const key =
					comment.parentId === null ? "_null_" : comment.parentId.toString();

				if (updatedComments[key]) {
					if (
						updatedComments[key].findIndex((obj) => obj.id === comment.id) ===
						-1
					) {
						updatedComments[key].push(comment);
					}
				} else {
					updatedComments[key] = [comment];
				}

				if (newComment && comment.parentId !== null) {
					// Update parent object in a non-mutating way
					for (const k in updatedComments) {
						let objIndex = updatedComments[k].findIndex(
							(obj) => obj.id === comment.parentId
						);
						if (objIndex !== -1) {
							let obj = updatedComments[k][objIndex];

							const updatedObj = {
								...obj,
								childrenCount: obj.childrenCount + 1,
							};

							updatedComments[k] = [
								...updatedComments[k].slice(0, objIndex),
								updatedObj,
								...updatedComments[k].slice(objIndex + 1),
							];
						}
					}
				}
			});

			return updatedComments;
		});
	}

	async function getChildren(id: number, count: number): Promise<void> {
		let res = getCommentsByParentId(id);
		console.log(res, count);
		if (res.length < count) {
			let data = await getChildrenAsync(id);
			if (data && data.data?.comments) res = data.data.comments;
			addComments(res, false);
		}
	}
	const getCommentsByParentId = (parentId: number | null): Comment[] => {
		const key = parentId === null ? "_null_" : parentId.toString();

		return comments[key] || [];
	};
	function responseHandler(id: number) {
		setResponseId(id);
		setModal(true);
	}
	async function setOpenCommentStatus(
		id: number,
		status: boolean,
		count: number
	): Promise<void> {
		console.log(id, status, count);
		await getChildren(id, count);
		if (status) {
			setOpenComments([...openComments, id]);
		} else {
			setOpenComments(openComments.filter((commentId) => commentId !== id));
		}
	}
	useEffect(() => {
		console.log(topLayerComments);
		if (topLayerComments && topLayerComments.comments) {
			addComments(topLayerComments.comments, false);
			setTotalPages(topLayerComments.totalPages);
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
				fileURL: commentTmp.FileURL,
				createdAt: commentTmp.CreatedAt,
				id: commentTmp.Id,
				childrenCount: commentTmp.ChildrenCount,
				parentId: commentTmp.ParentId,
			};
			console.log(comment);
			addComments([comment], true);
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
		<div style={{ width: "75vw", minWidth: "700px", margin: "auto" }}>
			<button
				onClick={() => {
					setResponseId(null);
					setModal(true);
				}}
				style={{ marginBottom: "20px", padding: "10px", borderRadius: "8px" }}
			>
				Write a comment
			</button>
			<div
				style={{
					marginBottom: "20px",
					display: "flex",
					gap: "5px",
					justifyContent: "center",
				}}
			>
				{Array.from({ length: totalPages }, (_, i) => (
					<button
						key={i + 1}
						onClick={() => {
							setComments({});
							setPage(i + 1);
						}}
						style={{
							padding: "10px",
							borderRadius: "8px",
							background: i + 1 === page ? "gray" : "white",
							color: i + 1 === page ? "white" : "black",
							width: "39px",
						}}
					>
						{i + 1}
					</button>
				))}
			</div>
			<CommentsList
				handleResponse={responseHandler}
				setOpenCommentStatus={setOpenCommentStatus}
				id={null}
				getCommentsByParentId={getCommentsByParentId}
				setFileToView={setFileToView}
			/>
			{modal && <CommentForm parentId={responseId} setModal={setModal} />}
			{fileToView && (
				<FileViewer setOpen={setFileToView} fileUrl={fileToView} />
			)}
		</div>
	);
};

export default Websocket;
