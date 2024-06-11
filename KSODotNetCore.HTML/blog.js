const tblBlogs = "blogs";
let currBlogId = null;

function readBlog() {
  const blogs = localStorage.getItem(tblBlogs);
  console.log(blogs);
}

// Edit Function
function editBlog(id) {
  id = $()
  const list = getBlogs();
  const item = list.find((x) => x.id === id);
  if (item === undefined) {
    console.log("No data found");
    errorMessage("No data found");
    return;
  }

  $("#txtTitle").val(item.title);
  $("#txtAuthor").val(item.author);
  $("#txtContent").val(item.content);
  $("#txtTitle").focus();

  currBlogId = item.id;
}

// Create Function
function createBlog(title, author, content) {
  const list = getBlogs();
  const requestModel = {
    id: uuidv4(),
    title: title,
    author: author,
    content: content,
  };

  list.push(requestModel);

  const jsonBlogs = JSON.stringify(list);
  localStorage.setItem(tblBlogs, jsonBlogs);

  successMessage("Saving Successful");
  getBlogTable();
  clearControls();
}

// Update Function
function updateBlog(id, title, author, content) {
  const list = getBlogs();
  const item = list.find((x) => x.id === id);
  if (item === undefined) {
    console.log("No data found");
    errorMessage("No data found");
    return;
  }

  item.title = title;
  item.author = author;
  item.content = content;

  const jsonBlogs = JSON.stringify(list);
  localStorage.setItem(tblBlogs, jsonBlogs);

  successMessage("Updating Successful");
  getBlogTable();
  clearControls();
}

// Delete Function
function deleteBlog(id) {
  confirmMessage("Are you sure you want to delete?").then(
    () => {
      let list = getBlogs();

      const item = list.find((x) => x.id === id);
      if (item === undefined) {
        console.log("No data found");
        errorMessage("No data found");
        return;
      }

      list = list.filter((x) => x.id !== id);

      const jsonBlogs = JSON.stringify(list);
      localStorage.setItem(tblBlogs, jsonBlogs);

      successMessage("Deleting Successful");
      getBlogTable();
      clearControls();
    },
    () => {
      return;
    }
  )
}



// Get Blogs from localStorage function
function getBlogs() {
  const blogs = localStorage.getItem(tblBlogs);
  const list = blogs === null ? [] : JSON.parse(blogs);
  return list;
}

// Click Events
$("#btnSave").click(function () {
  const title = $("#txtTitle").val();
  const author = $("#txtAuthor").val();
  const content = $("#txtContent").val();

  if (currBlogId === null) {
    createBlog(title, author, content);
  } else {
    updateBlog(currBlogId, title, author, content);
    blogId = null;
  }
});

// Form Controls reset
function clearControls() {
  $("#txtTitle").val("");
  $("#txtAuthor").val("");
  $("#txtContent").val("");
  $("#txtTitle").focus();
}

// For Table Refreshing
function getBlogTable() {
  const list = getBlogs();
  let count = 0;
  let htmlRows = "";
  list.forEach((item) => {
    const htmlRow = `
    <tr>
      <td>
      <button type="button" class="btn btn-warning" data-id="${item.id}" onclick="editBlog('${
        item.id
      }')">Edit</button>
      <button type="button" class="btn btn-danger" data-blog-id="${item.id}" onclick="deleteBlog('${
        item.id
      }')">Delete</button>
      </td>
      <td>${++count}</td>
      <td>${item.title}</td>
      <td>${item.author}</td>
      <td>${item.content}</td>
    </tr>
  `;
    htmlRows += htmlRow;
  });

  $("#tbodyBlogs").html(htmlRows);
}

// Initializations
getBlogTable();
