import React, { useEffect } from "react";

import Comment from "../Comment.tsx";
interface CommentsListProps {
	comments: Comment[];
}
const CommentsList: React.FC<CommentsListProps> = ({ comments }) => {
	return (
		<div
			style={{ display: "flex", flexDirection: "column", alignItems: "end" }}
		>
			{comments &&
				comments.map((comment) => (
					<Comment key={comment.id} comment={comment} />
				))}
		</div>
	);
};

export default CommentsList;
