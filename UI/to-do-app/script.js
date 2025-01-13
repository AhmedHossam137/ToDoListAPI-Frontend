const apiBaseUrl = "https://localhost:7026/api/ToDoTasks";

// Fetch and display tasks
async function fetchTasks() {
  try {
    const response = await fetch(apiBaseUrl);
    const tasks = await response.json();
    displayTasks(tasks);
  } catch (error) {
    console.error("Error fetching tasks:", error);
  }
}

// Display tasks in the list
function displayTasks(tasks) {
  const taskList = document.getElementById("tasks");
  taskList.innerHTML = ""; // Clear the list first
  tasks.forEach(task => {
    const li = document.createElement("li");
    li.innerHTML = `
      <span>${task.title} - ${task.status || 'Pending'}</span> <!-- Default status -->
      <button onclick="deleteTask(${task.id})">Delete</button>
    `;
    taskList.appendChild(li);
  });
}

// Add a new task
document.getElementById("add-task-form").addEventListener("submit", async (event) => {
  event.preventDefault();
  const title = document.getElementById("title").value;
  const description = document.getElementById("description").value;
  const priority = document.getElementById("priority").value;
  const dueDate = document.getElementById("due-date").value;

  const newTask = {
    title,
    description,
    priority: parseInt(priority), // Make sure it's an integer
    dueDate: dueDate ? new Date(dueDate) : null // If no due date, pass null
  };

  try {
    const response = await fetch(apiBaseUrl, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(newTask),
    });

    if (response.ok) {
      fetchTasks(); // Refresh the task list after adding a new task
    } else {
      console.error("Failed to add task:", await response.text());
    }
  } catch (error) {
    console.error("Error adding task:", error);
  }
});

// Delete a task
async function deleteTask(id) {
  try {
    await fetch(`${apiBaseUrl}/${id}`, {
      method: "DELETE",
    });
    fetchTasks(); // Refresh the list after deletion
  } catch (error) {
    console.error("Error deleting task:", error);
  }
}

// Fetch a task by ID
async function fetchTaskById() {
  const taskId = document.getElementById("task-id").value;

  if (!taskId) {
    alert("Please enter a task ID.");
    return;
  }

  try {
    const response = await fetch(`${apiBaseUrl}/${taskId}`);
    if (response.ok) {
      const task = await response.json();
      displayTaskDetails(task);
    } else {
      alert("Task not found.");
    }
  } catch (error) {
    console.error("Error fetching task by ID:", error);
  }
}

// Display the task details
function displayTaskDetails(task) {
  const taskDetailsDiv = document.getElementById("task-details");
  taskDetailsDiv.innerHTML = `
    <h3>Task Details</h3>
    <p><strong>Title:</strong> ${task.title}</p>
    <p><strong>Description:</strong> ${task.description}</p>
    <p><strong>Priority:</strong> ${task.priority}</p>
    <p><strong>Due Date:</strong> ${task.dueDate}</p>
    <p><strong>Status:</strong> ${task.status || 'Pending'}</p>
  `;
}

// Initial fetch
fetchTasks();
