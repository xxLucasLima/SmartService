
function parseDateToLocal(date) {
    return new Date(date.substring(0, 10)).toLocaleDateString()
}
export { parseDateToLocal };
