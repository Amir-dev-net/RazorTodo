using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorTodo.Pages
{
    public class ToDoModel : PageModel
    {
        private static List<ToDoItem> ToDoList = new();

        public List<ToDoItem> Items => ToDoList;

        public void OnGet()
        {
            // Load default tasks only if the list is empty (prevents duplicates on refresh)
            if (ToDoList.Count == 0)
            {
                ToDoList.AddRange(new List<ToDoItem>
            {
                new ToDoItem { Id = 1, Title = "Morning Workout(Running)", IsCompleted = false },
                new ToDoItem { Id = 2, Title = "Read a Book (30 mins)", IsCompleted = false },
                new ToDoItem { Id = 3, Title = "Plan Weekly Meals", IsCompleted = false },
                new ToDoItem { Id = 4, Title = "Meditate for 10 Minutes", IsCompleted = false },
                new ToDoItem { Id = 5, Title = "Drink 8 Glasses of Water", IsCompleted = false },
                new ToDoItem { Id = 6, Title = "Do some researches on tech", IsCompleted = false }
            });
            }
        }

        public IActionResult OnPostAdd(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                ToDoList.Add(new ToDoItem { Id = ToDoList.Count + 1, Title = title, IsCompleted = false });
            }
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
