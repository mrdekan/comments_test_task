import React, { SetStateAction, FC, useState, useEffect } from "react";
import cl from "./FileViewer.module.css";
import { Dispatch } from "@reduxjs/toolkit";
import { AnimatePresence, motion, transform } from "framer-motion";
interface FileViewerProps {
	setOpen: Dispatch<SetStateAction<string | null>>;
	fileUrl: string;
}
const FileViewer: FC<FileViewerProps> = ({ setOpen, fileUrl }) => {
	const PHOTO_URL = process.env.REACT_APP_PHOTO_URL;
	const [fileContent, setFileContent] = useState<string | null>(null);
	const [error, setError] = useState<string | null>(null);

	const handleLoadFile = async () => {
		const filePath = PHOTO_URL + fileUrl;

		try {
			const response = await fetch(filePath);
			if (!response.ok) {
				throw new Error("Failed to fetch file");
			}
			const content = await response.text();
			setFileContent(content);
			setError(null);
		} catch (err) {
			setError(
				"Error: " + (err instanceof Error ? err.message : "Unknown error")
			);
			setFileContent(null);
		}
	};
	useEffect(() => {
		if (fileUrl.endsWith(".txt")) {
			handleLoadFile();
		}
	}, [fileUrl]);
	useEffect(() => {
		const handleEscKey = (event) => {
			if (event.key === "Escape") {
				setOpen(null);
			}
		};

		window.addEventListener("keydown", handleEscKey);

		return () => {
			window.removeEventListener("keydown", handleEscKey);
		};
	}, []);
	return (
		<AnimatePresence>
			<motion.div
				initial={{ opacity: 0 }}
				animate={{ opacity: 1 }}
				transition={{ duration: 0.2 }}
				className={[cl.background, "closeOnClick"].join(" ")}
				onMouseDown={(event) => {
					if (event.target.classList.contains("closeOnClick")) setOpen(null);
				}}
			>
				<motion.div
					initial={{ transform: "scale(0)" }}
					animate={{ transform: "scale(1)" }}
					transition={{ duration: 0.2 }}
				>
					{fileUrl.endsWith(".txt") ? (
						<div
							style={{
								padding: "20px",
								backgroundColor: "white",
								fontSize: "22px",
								maxHeight: "95vh",
								overflowY: "auto",
								maxWidth: "90vw",
							}}
						>
							<p style={{ textAlign: "start", marginBottom: "20px" }}>
								{fileUrl}
							</p>
							<div
								style={{
									height: "1px",
									marginBottom: "20px",
									backgroundColor: "lightgray",
								}}
							></div>
							{fileContent && (
								<pre style={{ textAlign: "start" }}>{fileContent}</pre>
							)}
							{error && <p style={{ color: "red" }}>{error}</p>}
						</div>
					) : (
						<img src={PHOTO_URL + fileUrl} alt="file" />
					)}
				</motion.div>
			</motion.div>
		</AnimatePresence>
	);
};

export default FileViewer;
