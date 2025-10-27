import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import React from "react";

// Import your app
import App from "../../frontend/src/App.jsx";

// Mock the API used inside App.jsx
jest.mock("../../frontend/src/api.js", () => require("./__mocks__/api.mock.js"));
import { api } from "./__mocks__/api.mock.js";

const sample = (o = {}) => ({
  id: 1,
  title: "Version up",
  description: "take the docx",
  isCompleted: false,
  createdAt: "2025-10-24T03:45:10Z",
  ...o
});

describe("App (frontend)", () => {
  beforeEach(() => {
    jest.clearAllMocks();
  });

  test("renders and loads latest tasks", async () => {
    api.get.mockResolvedValueOnce({ data: [sample(), sample({ id: 2, title: "Code review" })] });

    render(<App />);

    expect(screen.getByText(/loading/i)).toBeInTheDocument();

    const items = await screen.findAllByText(/Version up|Code review/);
    expect(items.length).toBe(2);
    expect(api.get).toHaveBeenCalledWith("/api/tasks");
  });

  test("creates a task and reloads the list", async () => {
    api.get.mockResolvedValueOnce({ data: [] }); // first load()
    api.post.mockResolvedValueOnce({ status: 200 });
    api.get.mockResolvedValueOnce({ data: [sample({ title: "New Task" })] }); // after add()

    render(<App />);

    const title = await screen.findByPlaceholderText(/task title/i);
    const desc  = screen.getByPlaceholderText(/description/i);
    const add   = screen.getByRole("button", { name: /add task/i });

    await userEvent.type(title, "New Task");
    await userEvent.type(desc, "Notes here");
    await userEvent.click(add);

    expect(api.post).toHaveBeenCalledWith("/api/tasks", {
      title: "New Task",
      description: "Notes here"
    });
    expect(await screen.findByText("New Task")).toBeInTheDocument();
  });

  test("marks a task done and reloads", async () => {
    api.get.mockResolvedValueOnce({ data: [sample({ id: 50, title: "Finish" })] }); // first load
    api.put.mockResolvedValueOnce({ status: 204 });
    api.get.mockResolvedValueOnce({ data: [] }); // after done -> reload

    render(<App />);

    const doneBtn = await screen.findByRole("button", { name: /done/i });
    await userEvent.click(doneBtn);

    expect(api.put).toHaveBeenCalledWith("/api/tasks/50/done");
    expect(await screen.findByText(/no active tasks/i)).toBeInTheDocument();
  });

  test("shows an error banner when load fails", async () => {
    api.get.mockRejectedValueOnce(new Error("boom"));
    render(<App />);
    expect(await screen.findByText(/failed to load tasks/i)).toBeInTheDocument();
  });
});
