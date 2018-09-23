function createCommentItem(form, path) {
    var service = new ItemService({ url: '/sitecore/api/ssc/item' });
    var obj = {
        ItemName: 'comment - ' + form.name.value,
        TemplateID: '{C68FBD7B-B969-40E0-A4E8-2804B910E0AE}',
        Name: form.name.value,
        Comment: form.comment.value,
    };

    service.create(obj)
    .path(path)
    .execute()
    .then(function (item) {
        form.name.value = form.comment.value = '';
        window.alert('Thanks');
    })
    .fail(function (err) {
        window.alert(err);
    });
    return false;
}
