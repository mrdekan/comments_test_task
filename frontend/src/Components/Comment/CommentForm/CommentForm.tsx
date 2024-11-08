import React, { useState, SetStateAction, useRef } from "react";
import { usePostCommentMutation } from "../../../API/comments.ts";
import cl from "./CommentForm.module.css";
import { Dispatch } from "@reduxjs/toolkit";
interface CommentFormProps {
	setModal: Dispatch<SetStateAction<boolean>>;
	parentId: number | null;
}
const CommentForm: React.FC<CommentFormProps> = ({ setModal, parentId }) => {
	const API_URL = process.env.REACT_APP_API_URL;
	const textareaRef = useRef<HTMLTextAreaElement | null>(null);
	const [content, setContent] = useState("");
	const [username, setUsername] = useState("");
	const [email, setEmail] = useState("");
	const [homepage, setHomepage] = useState("");
	const [file, setFile] = useState<File | null>(null);
	const [captchaText, setCaptchaText] = useState("");
	const [errors, setErrors] = useState<string[]>([]);
	const [captchaUrl, setCaptchaUrl] = useState<string>("/captcha");
	const [postComment, { isLoading, isError, error }] = usePostCommentMutation();
	const tags = [
		{
			tag: "a",
			open: "<a href='' title=''>",
			close: "</a>",
		},
		{
			tag: "strong",
			open: "<strong>",
			close: "</strong>",
		},
		{
			tag: "i",
			open: "<i>",
			close: "</i>",
		},
		{
			tag: "code",
			open: "<code>",
			close: "</code>",
		},
	];
	const insertTag = (openTag: string, closeTag: string) => {
		if (textareaRef.current) {
			const { selectionStart, selectionEnd, value } = textareaRef.current;
			const selectedText = value.substring(selectionStart, selectionEnd);

			if (selectedText) {
				const newText =
					value.substring(0, selectionStart) +
					openTag +
					selectedText +
					closeTag +
					value.substring(selectionEnd);
				setContent(newText);
			} else {
				const newText =
					value.substring(0, selectionStart) +
					openTag +
					closeTag +
					value.substring(selectionEnd);
				setContent(newText);
			}
		}
	};
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
		await postComment(formData)
			.unwrap()
			.then((res) => {
				console.log("Comment posted:", res);
				setModal(false);
			})
			.catch((err) => {
				if (err.data.errors) setErrors(Object.values(err.data.errors).flat());
				console.error("Failed to post comment:", err);
			});
	};
	const handleCaptchaRefresh = () => {
		setCaptchaUrl(`/captcha?timestamp=${new Date().getTime()}`);
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
				<div style={{ display: "flex", gap: "5px" }}>
					{tags.map((tag) => (
						<button
							key={tag.tag}
							style={{ padding: "5px", borderRadius: "5px", minWidth: "35px" }}
							onClick={(e) => {
								e.preventDefault();
								insertTag(tag.open, tag.close);
							}}
						>
							{tag.tag}
						</button>
					))}
				</div>
				<textarea
					value={content}
					onChange={(e) => setContent(e.target.value)}
					placeholder="Write a comment"
					ref={textareaRef}
				/>
				<div style={{ display: "flex", alignItems: "center", gap: "5px" }}>
					<img src={API_URL + captchaUrl} alt="Captcha" />
					<div
						style={{
							display: "flex",
							alignItems: "center",
							gap: "5px",
							flexDirection: "column",
						}}
					>
						<button
							style={{ padding: "5px", borderRadius: "5px", minWidth: "35px" }}
							onClick={(e) => {
								e.preventDefault();
								handleCaptchaRefresh();
							}}
						>
							Update CAPTCHA
						</button>
						<input
							type="text"
							value={captchaText}
							onChange={(e) => setCaptchaText(e.target.value)}
							placeholder="Enter captcha"
						/>
					</div>
				</div>
				<button type="submit" disabled={isLoading}>
					{isLoading ? "Posting..." : "Post Comment"}
				</button>

				{errors.map((error, index) => (
					<p key={index} style={{ color: "red", textAlign: "center" }}>
						{error}
					</p>
				))}
			</form>
		</div>
	);
};

export default CommentForm;
