var imported = document.createElement('script');
imported.src = '/sitecore/shell/client/Services/Assets/lib/itemservice.js';
document.head.appendChild(imported);


function createCommentItem(form, path)
{
      var obj = {
        ItemName: 'comment -' + form.name.value,
        TemplateID: '{4D02A874-E467-4C0A-9F63-9853C45157F9}',
        Name: form.name.value,
        Comment: form.comment.value,

    };
    var service = new Itemservice({ url: '/sitecore/api/ssc/item' });

    service.create(obj)
        .path(path)
        .execute()
        .then(function (item) {
            form.name.value = form.comment.value = '';
            window.alert('thanks.Your message will show on the site shortly');
        })
        .fail(function (err) {
            window.alert(err);
        });
    event.preventDefault();
    return false;

}