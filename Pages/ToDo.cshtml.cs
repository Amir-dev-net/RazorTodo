using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorTodo.Pages
{
    public class ToDoModel : PageModel
    {
        private static List<ToDoItem> ToDoList = new();

        public List<ToDoItem> Items => ToDoList;

        [TempData]
        public string Message { get; set; }

        public void OnGet()
        {
            if (ToDoList.Count == 0)
            {
                ToDoList.AddRange(new List<ToDoItem>
            {
                new ToDoItem { Id = 1, Title = "Morning Workout (Running)", IsCompleted = false },
                new ToDoItem { Id = 2, Title = "Read a Book (30 mins)", IsCompleted = false },
                new ToDoItem { Id = 3, Title = "Plan Weekly Meals", IsCompleted = false },
                new ToDoItem { Id = 4, Title = "Meditate for 10 Minutes", IsCompleted = false },
                new ToDoItem { Id = 5, Title = "Drink 8 Glasses of Water", IsCompleted = false },
                new ToDoItem { Id = 6, Title = "Do some researches on technology", IsCompleted = false },
                new ToDoItem { Id = 7, Title = "Complete a coding challenge", IsCompleted = false },
                new ToDoItem { Id = 8, Title = "Write a blog post", IsCompleted = false },
                new ToDoItem { Id = 9, Title = "Practice a new programming language", IsCompleted = false },
                new ToDoItem { Id = 10, Title = "Organize workspace", IsCompleted = false },
                new ToDoItem { Id = 11, Title = "Review project backlog", IsCompleted = false },
                new ToDoItem { Id = 12, Title = "Plan next week's schedule", IsCompleted = false }
            });
            }
        }

        public IActionResult OnPostAdd(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                TempData["Message"] = "Task title cannot be empty.";
                TempData["MessageType"] = "danger"; // Red color for errors
                return RedirectToPage();
            }

            // Trim spaces and check for duplicates (case-insensitive)
            string trimmedTitle = title.Trim();
            if (ToDoList.Any(item => item.Title.Trim().Equals(trimmedTitle, System.StringComparison.OrdinalIgnoreCase)))
            {
                TempData["Message"] = "This task is already present!";
                TempData["MessageType"] = "warning"; // Yellow color for duplicate warnings
                return RedirectToPage();
            }

            ToDoList.Add(new ToDoItem { Id = ToDoList.Count + 1, Title = trimmedTitle, IsCompleted = false });
            TempData["Message"] = "Task added successfully!";
            TempData["MessageType"] = "success"; // Green color for success

            return RedirectToPage();
        }



        public IActionResult OnPostComplete(int id)
        {
            var item = ToDoList.Find(t => t.Id == id);
            if (item != null) item.IsCompleted = !item.IsCompleted;
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            ToDoList.RemoveAll(t => t.Id == id);
            return RedirectToPage();
        }
    }

    public class ToDoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
