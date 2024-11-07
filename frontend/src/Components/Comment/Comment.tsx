import React, { FC } from "react";
interface CommentProps {
	comment: Comment;
}
const Comment: FC<CommentProps> = ({ comment }) => {
	function formatDate(dateString) {
		const date = new Date(dateString);

		const day = String(date.getDate()).padStart(2, "0");
		const month = String(date.getMonth() + 1).padStart(2, "0");
		const year = date.getFullYear();
		const hours = String(date.getHours()).padStart(2, "0");
		const minutes = String(date.getMinutes()).padStart(2, "0");

		return `${day}.${month}.${year} в ${hours}:${minutes}`;
	}
	return (
		<div style={{ width: "90%" }}>
			<div
				style={{
					display: "flex",
					alignItems: "center",
					gap: "10px",
					backgroundColor: "lightblue",
					padding: "10px",
				}}
			>
				<div>
					<p>{comment.author.username}</p>
					<p>{comment.author.email}</p>
				</div>
				<div
					style={{
						display: "flex",
						justifyContent: "space-between",
						width: "100%",
					}}
				>
					<p>{formatDate(comment.createdAt)}</p>
					<p>{comment.childrenCount}</p>
				</div>
			</div>
			<div style={{ padding: "10px" }}>
				<p>{comment.content}</p>
				<p>{comment.id}</p>
			</div>
		</div>
	);
};

export default Comment;
