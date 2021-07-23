var selectList = {};

selectList.SetSelectedItem = function (elementId, valueToSelect) {
    let element = document.getElementById(elementId);
    element.value = valueToSelect;
}