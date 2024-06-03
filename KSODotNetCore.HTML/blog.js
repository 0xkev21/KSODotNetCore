const tblBlogs = "blogs";
let currBlogId = null;

function readBlog() {
  const blogs = localStorage.getItem(tblBlogs);
  console.log(blogs);
}

// Edit Function
function editBlog(id) {
  const list = getBlogs();
  const item = list.find(x => x.id === id);
  if(item === undefined) {
    console.log("No data found");
    errorMessage("No data found");
    return;
  }

  $('#txtTitle').val(item.title);
  $('#txtAuthor').val(item.author);
  $('#txtContent').val(item.content);
  $('#txtTitle').focus();

  currBlogId = item.id;
}

// Create Function
function createBlog(title, author, content) {
  const list = getBlogs();
  const requestModel = {
    id: uuidv4(),
    title: title,
    author: author,
    content: content
  }

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
  const item = list.find(x => x.id === id);
  if(item === undefined) {
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
  let result = confirm("Are you sure to delete?");
  if(!result) return;
  
  let list = getBlogs();

  const item = list.find(x => x.id === id);
  if(item === undefined) {
    console.log("No data found");
    errorMessage("No data found");
    return;
  }

  list = list.filter(x => x.id !== id);

  const jsonBlogs = JSON.stringify(list);
  localStorage.setItem(tblBlogs, jsonBlogs);

  successMessage("Deleting Successful");
  getBlogTable();
  clearControls();
}

// random uuid generator
function uuidv4() {
  return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
    (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
  );
}

// Get Blogs from localStorage function
function getBlogs() {
  const blogs = localStorage.getItem(tblBlogs);
  const list = (blogs === null) ? [] : JSON.parse(blogs);
  return list;
}


// Click Events
$('#btnSave').click(function() {
  const title = $('#txtTitle').val();
  const author = $('#txtAuthor').val();
  const content = $('#txtContent').val();

  if(currBlogId === null) {
    createBlog(title, author, content);
  } else {
    updateBlog(currBlogId, title, author, content);
    blogId = null;
  }
})

// Messages
function successMessage(message) {
  alert(message);
}
function errorMessage(message) {
  alert(message);
}

// Form Controls reset
function clearControls() {
  $('#txtTitle').val('');
  $('#txtAuthor').val('');
  $('#txtContent').val('');
  $('#txtTitle').focus();
}

// For Table Refreshing
function getBlogTable() {
  const list = getBlogs();
  let count = 0;
  let htmlRows = '';
  list.forEach(item => {
    const htmlRow = `
    <tr>
      <td>
      <button type="button" class="btn btn-warning" onclick="editBlog('${item.id}')">Edit</button>
      <button type="button" class="btn btn-danger" onclick="deleteBlog('${item.id}')">Delete</button>
      </td>
      <td>${++count}</td>
      <td>${item.title}</td>
      <td>${item.author}</td>
      <td>${item.content}</td>
    </tr>
  `;
  htmlRows += htmlRow;
  });

  $('#tbodyBlogs').html(htmlRows);
}

// Initializations
getBlogTable();