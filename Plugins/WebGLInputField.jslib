mergeInto(LibraryManager.library, {
  IsTouchDevice: function () {
      return (('ontouchstart' in window) ||
          (navigator.maxTouchPoints > 0) ||
          (navigator.msMaxTouchPoints > 0));
  },
  RequestNativePopUp: function (sender, description, currentText) {
    let promptField = window.prompt(UTF8ToString(description), UTF8ToString(currentText));
    let text;
    if (promptField == null || promptField === "") {
      text = UTF8ToString(currentText);
    } else {
      text = promptField;
    }
    window.focus();
    SendMessage(UTF8ToString(sender), 'ReceiveInputFieldText', String(text));
  }
});
