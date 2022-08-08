function addTableRow(tablename, listname, fields) {
    var table = $("#" + tablename);
    var i = table.children("tbody").children("tr").length;
    var insert = '<tr>';
    var id = '';
    fields.forEach(e => {
        if (e.endsWith('Id')) {
            id += '<input type="hidden" id="' + listname + '_' + i + '__' + e + '" name="' + listname + '[' + i + '].' + e + '" class="form-control">';
        }
        else {
            insert += '<td>' + id + '<input type="text" id="' + listname + '_' + i + '__' + e + '" name="' + listname + '[' + i + '].' + e + '" class="form-control">';
            id = '';
        }
    })
    insert += '<td><button></button></td></tr>';
    table.append(insert);
    return false;
}