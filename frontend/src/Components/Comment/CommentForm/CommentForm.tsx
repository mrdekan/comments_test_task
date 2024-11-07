import React, { useState, SetStateAction } from "react";
import { usePostCommentMutation } from "../../../API/comments.ts";
import cl from "./CommentForm.module.css";
import { Dispatch } from "@reduxjs/toolkit";
interface CommentFormProps {
	setModal: Dispatch<SetStateAction<boolean>>;
	parentId: number | null;
}
const CommentForm: React.FC<CommentFormProps> = ({ setModal, parentId }) => {
	const API_URL = process.env.REACT_APP_API_URL;
	const [content, setContent] = useState("");
	const [username, setUsername] = useState("");
	const [email, setEmail] = useState("");
	const [homepage, setHomepage] = useState("");
	const [file, setFile] = useState<File | null>(null);
	const [captchaText, setCaptchaText] = useState("");

	const [postComment, { isLoading, isError, error }] = usePostCommentMutation();

	const handleSubmit = async (e: React.FormEvent) => {
		e.preventDefault();
		const formData = new FormData();
		formData.append("content", content);
		formData.append("username", username);
		formData.append("email", email);
		if (parentId !== null) formData.append("parentId", parentId);
		if (homepage) formData.append("homepage", homepage);
		if (file) formData.append("file", file);
		formData.append("captchaText", captchaText);

		try {
			const response = await postComment(formData).unwrap();
			console.log("Comment posted:", response);
			setModal(false);
		} catch (err) {
			console.error("Failed to post comment:", error);
		}
	};

	return (
		<div
			className={[cl.modalBackground, "closeOnClick"].join(" ")}
			onMouseDown={(event) => {
				if (event.target.classList.contains("closeOnClick")) setModal(false);
			}}
		>
			<form onSubmit={handleSubmit}>
				<input
					type="text"
					value={username}
					onChange={(e) => setUsername(e.target.value)}
					placeholder="Username"
				/>
				<input
					type="email"
					value={email}
					onChange={(e) => setEmail(e.target.value)}
					placeholder="Email"
				/>
				<input
					type="text"
					value={homepage}
					onChange={(e) => setHomepage(e.target.value)}
					placeholder="Homepage (optional)"
				/>
				<input
					type="file"
					onChange={(e) => setFile(e.target.files ? e.target.files[0] : null)}
				/>
				<textarea
					value={content}
					onChange={(e) => setContent(e.target.value)}
					placeholder="Write a comment"
				/>
				<div style={{ display: "flex", alignItems: "center", gap: "5px" }}>
					<img src={API_URL + "/captcha"} alt="Captcha" />
					<input
						type="text"
						value={captchaText}
						onChange={(e) => setCaptchaText(e.target.value)}
						placeholder="Enter captcha"
					/>
				</div>
				<button type="submit" disabled={isLoading}>
					{isLoading ? "Posting..." : "Post Comment"}
				</button>

				{isError && (
					// @ts-ignore
					<div>Failed to post comment: {error.message || "Unknown error"}</div>
				)}
			</form>
		</div>
	);
};

export default CommentForm;
