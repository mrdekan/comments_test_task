import React, { useState } from "react";
import { usePostCommentMutation } from "../../../API/comments.ts";

const CommentForm: React.FC = () => {
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

		// Формуємо FormData
		const formData = new FormData();
		formData.append("content", content);
		formData.append("username", username);
		formData.append("email", email);
		if (homepage) formData.append("homepage", homepage);
		if (file) formData.append("file", file);
		formData.append("captchaText", captchaText);

		try {
			// Викликаємо RTK Query для відправки коментаря
			const response = await postComment(formData).unwrap();
			console.log("Comment posted:", response);
		} catch (err) {
			console.error("Failed to post comment:", error);
		}
	};

	return (
		<form onSubmit={handleSubmit}>
			<textarea
				value={content}
				onChange={(e) => setContent(e.target.value)}
				placeholder="Write a comment"
			/>
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
			<img src={API_URL + "/captcha"} alt="Captcha" />
			<input
				type="text"
				value={captchaText}
				onChange={(e) => setCaptchaText(e.target.value)}
				placeholder="Enter captcha"
			/>
			<button type="submit" disabled={isLoading}>
				{isLoading ? "Posting..." : "Post Comment"}
			</button>

			{isError && (
				// @ts-ignore
				<div>Failed to post comment: {error.message || "Unknown error"}</div>
			)}
		</form>
	);
};

export default CommentForm;
