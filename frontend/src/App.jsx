import { useEffect, useMemo, useState } from "react";
import { api } from "./api";
import "./styles.css";

export default function App() {
  const [tasks, setTasks] = useState([]);
  const [title, setTitle]   = useState("");
  const [description, setDescription] = useState("");
  const [loading, setLoading] = useState(true);
  const [saving, setSaving]   = useState(false);
  const [error, setError]     = useState("");

  const canSubmit = useMemo(() => title.trim().length > 0 && !saving, [title, saving]);

  async function load(){
    try{
      setLoading(true);
      const { data } = await api.get("/api/tasks");
      setTasks(data);
      setError("");
    }catch{
      setError("Failed to load tasks. Is the API running on http://localhost:5000 ?");
    }finally{
      setLoading(false);
    }
  }

  async function add(e){
    e.preventDefault();
    if(!canSubmit) return;
    try{
      setSaving(true);
      await api.post("/api/tasks", { title: title.trim(), description });
      setTitle(""); setDescription("");
      await load();
    }catch{
      setError("Failed to add task.");
    }finally{
      setSaving(false);
    }
  }

  async function done(id){
    try{
      await api.put(`/api/tasks/${id}/done`);
      await load();
    }catch{
      setError("Failed to mark task as done.");
    }
  }

  useEffect(() => { load(); }, []);

  return (
    <div className="container">
      {/* Top bar */}
      <div className="nav">
        <div className="brand">✨ To-Do</div>
        <div className="badge">Latest 5 • Active Only</div>
      </div>

      {/* Entry card */}
      <div className="card">
        <form onSubmit={add} className="row">
          <input
            className="input"
            placeholder="Task title (required)"
            value={title}
            onChange={e => setTitle(e.target.value)}
            required
          />
          <textarea
            className="textarea"
            placeholder="Description (optional)"
            value={description}
            onChange={e => setDescription(e.target.value)}
          />
          <button className="btn btn-primary btn-wide" disabled={!canSubmit}>
            {saving ? "Adding…" : "Add Task"}
          </button>
        </form>

        {error && (
          <div style={{marginTop:12}}>
            <button className="btn btn-ghost" type="button">⚠ {error}</button>
          </div>
        )}
      </div>

      <div className="section-title">Recent</div>

      {loading ? (
        <div className="card">Loading…</div>
      ) : (
        <div className="list">
          {tasks.length === 0 && <div className="card">No active tasks. Add one above ✍️</div>}
          {tasks.map(t => (
            <div key={t.id} className="task">
              <div>
                <div className="task-title">{t.title}</div>
                {t.description && <div className="task-desc">{t.description}</div>}
                <div className="task-meta">Created {new Date(t.createdAt).toLocaleString()}</div>
              </div>
              <button className="btn btn-success" onClick={() => done(t.id)}>
                ✓ Done
              </button>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}
