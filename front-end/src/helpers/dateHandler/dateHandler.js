
function parseDateToLocal(date) {
    var dateSub = date.substring(0, 10);
    var newDate = new Date(dateSub).toLocaleDateString()
    if(newDate == "Invalid Date"){
        return date
    }
    else {
        return newDate
    }
}
export { parseDateToLocal };
