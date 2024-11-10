import React, { FC, Dispatch, SetStateAction, useState } from "react";
import CommentsList from "./CommentsList/CommentsList.tsx";
import cl from "./Comment.module.css";
interface CommentProps {
	comment: Comment;
	handleResponse?: (id: number) => void;
	setOpenCommentStatus?: (
		id: number,
		status: boolean,
		count: number
	) => Promise<void>;
	getCommentsByParentId?: (parentId: number | null) => Comment[];
	setFileToView?: Dispatch<SetStateAction<string | null>>;
}
const Comment: FC<CommentProps> = ({
	comment,
	handleResponse,
	setOpenCommentStatus,
	getCommentsByParentId,
	setFileToView,
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
						<p style={{ marginTop: "10px" }}>
							{comment.author.email}{" "}
							{comment.author.homepage && (
								<p>
									<a href={comment.author.homepage}>
										{comment.author.homepage}
									</a>
								</p>
							)}
						</p>
					</div>
					{comment.fileURL && (
						<div
							style={{ cursor: "pointer" }}
							onClick={() => setFileToView(comment.fileURL)}
						>
							{!comment.fileURL.endsWith(".txt") ? (
								<img
									src={PHOTO_URL + comment.fileURL}
									style={{ maxWidth: "100px", maxHeight: "100px" }}
									alt={comment.fileURL}
								/>
							) : (
								<img
									src={process.env.PUBLIC_URL + "/file.png"}
									style={{ maxWidth: "100px", maxHeight: "100px" }}
									alt={comment.fileURL}
								/>
							)}
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
					<button
						onClick={() => {
							if (handleResponse) handleResponse(comment.id);
						}}
					>
						Reply
					</button>
				</div>
			</div>
			{open && comment.childrenCount > 0 && (
				<CommentsList
					setOpenCommentStatus={setOpenCommentStatus}
					handleResponse={handleResponse}
					id={comment.id}
					getCommentsByParentId={getCommentsByParentId}
					setFileToView={setFileToView}
				/>
			)}
		</div>
	);
};

export default Comment;
