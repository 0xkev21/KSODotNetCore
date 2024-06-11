// random uuid generator
function uuidv4() {
  return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, (c) =>
    (
      +c ^
      (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (+c / 4)))
    ).toString(16),
  );
}

// Messages
function successMessage(message) {
  Swal.fire({
    title: "Success!",
    text: message,
    icon: "success",
  });
}
function errorMessage(message) {
  Swal.fire({
    title: "Error!",
    text: message,
    icon: "error",
  });
}

function confirmMessage(message) {
  const confirmMessage = new Promise((resolve, reject) => {
    Notiflix.Confirm.show(
      "Confirm Delete",
      message,
      "Delete",
      "Cancel",
      () => {
        resolve();
      },
      () => {
        reject();
      },
      {
        okButtonBackground: 'red'
      }
    )
  });
  return confirmMessage;
}