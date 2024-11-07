import React, { FC, useEffect, useState } from "react";
import CommentsList from "./CommentsList/CommentsList.tsx";
import cl from "./Comment.module.css";
interface CommentProps {
	comment: Comment;
	comments: { [key: string]: Comment[] };
	handleResponse: (id: number) => void;
	setOpenCommentStatus: (
		id: number,
		status: boolean,
		count: number
	) => Promise<void>;
	getCommentsByParentId: (parentId: number | null) => Comment[];
}
const Comment: FC<CommentProps> = ({
	comment,
	handleResponse,
	setOpenCommentStatus,
	getCommentsByParentId,
	comments,
}) => {
	const PHOTO_URL = process.env.REACT_APP_PHOTO_URL;
	const [open, setOpen] = useState<boolean>(false);
	function formatDate(dateString) {
		const date = new Date(dateString);

		const day = String(date.getDate()).padStart(2, "0");
		const month = String(date.getMonth() + 1).padStart(2, "0");
		const year = date.getFullYear();
		const hours = String(date.getHours()).padStart(2, "0");
		const minutes = String(date.getMinutes()).padStart(2, "0");

		return `${day}.${month}.${year} в ${hours}:${minutes}`;
	}
	function handleOpen() {
		setOpenCommentStatus(comment.id, !open, comment.childrenCount);
		setOpen(!open);
	}
	console.log(comment.fileURL);
	return (
		<div className={cl.comment}>
			<div className={cl.commentHeader}>
				<p style={{ fontWeight: "bold", fontSize: "18px" }}>
					{comment.author.username}
				</p>
				<div
					style={{
						display: "flex",
						justifyContent: "space-between",
						width: "100%",
					}}
				>
					<p>{formatDate(comment.createdAt)}</p>
					<p>{comment.childrenCount} replies</p>
				</div>
			</div>
			<div className={cl.commentBody}>
				<div style={{ display: "flex" }}>
					<div style={{ width: "100%" }}>
						<p
							style={{ fontSize: "16px" }}
							dangerouslySetInnerHTML={{ __html: comment.content }}
						/>
						<p style={{ marginTop: "10px" }}>{comment.author.email}</p>
					</div>
					{comment.fileURL && (
						<div>
							<img
								src={PHOTO_URL + comment.fileURL}
								style={{ maxWidth: "100px", maxHeight: "100px" }}
								alt={comment.fileURL}
							/>
						</div>
					)}
				</div>
				<div className={cl.buttons}>
					{comment.childrenCount > 0 ? (
						<button onClick={() => handleOpen()}>
							{!open
								? `View replies (${comment.childrenCount})`
								: `Hide replies`}
						</button>
					) : (
						<div></div>
					)}
					<button onClick={() => handleResponse(comment.id)}>Reply</button>
				</div>
			</div>
			{open && comment.childrenCount > 0 && (
				<CommentsList
					comments={comments}
					setOpenCommentStatus={setOpenCommentStatus}
					handleResponse={handleResponse}
					id={comment.id}
					getCommentsByParentId={getCommentsByParentId}
				/>
			)}
		</div>
	);
};

export default Comment;
