var noteid = -1;
$(function () {
    $('#showComments').on('show.bs.modal', function (e) {
        /*do something...*/
        console.log("deneme");
        var btn = $(e.relatedTarget); /* ilgili butonu bu satırda yakalıyoruz.*/
        noteid = btn.data("note-id"); /* buton içinde tanımladığımız data - note - id isimli özelliğe ait veriyi bu şekilde alabiliyorum.*/
        $('#modal-comment-body').load("/Comment/ShowNoteComments/" + noteid)
    });
});


function doComment(btn, e, commentid, spanid) {
    var button = $(btn); /* Gelen nesneyi Jquery tipine çeviriyorum.Bunu da aşağıda Jquery metotları ile kullanacağım.*/
    var mode = button.data("edit-mode");

    if (e === 'edit_clicked') {
        if (!mode) {
            button.data("edit-mode", true);
            button.removeClass("btn-warning");
            button.addClass("btn-success");
            var buttoni = button.find("i");
            buttoni.removeClass("bi-pencil");
            buttoni.addClass("bi-check-lg");
            $(spanid).attr("contenteditable", true);
            $(spanid).focus();
        }
        else {
            button.data("edit-mode", false);
            button.addClass("btn-warning");
            button.removeClass("btn-success");
            var buttoni = button.find("i");
            buttoni.addClass("bi-pencil");
            buttoni.removeClass("bi-check-lg");
            $(spanid).attr("contenteditable", false);

            /* Controller'a güncellenen yorumu gönderiyoruz ve veritabanına kayıt işlemi yapılıyor.*/

            var txt = $(spanid).text();

            $.ajax({
                method: "POST",
                url: "/Comment/Edit/" + commentid,
                data: { text: txt }
            }).done(function (data) {
                /* işlem başarılıysa  buradaki kodlar çalışacak */
                if (data.result) {
                    $('#modal-comment-body').load("/Comment/ShowNoteComments/" + noteid)
                } else {
                    alert("Yorum güncellenemedi..");
                }

            }).fail(function () {
                alert("Sunucuyla bağlantı kurulamadı..");
            });
        }
    }
    else if (e === 'delete_clicked') {
        var dialogResult = confirm("Yorum silinsin mi?");
        if (!dialogResult) { return false; }

        $.ajax({
            method: "GET",
            url: "/Comment/Delete/" + commentid
        }).done(function (data) {
            if (data.result) {
                // yorumları tekrardan yükleyeceğiz.
                $('#modal-comment-body').load("/Comment/ShowNoteComments/" + noteid);
            }
            else {
                alert("Yorum silinemedi..");
            }

        }).fail(function () {
            alert("Sunucuyla bağlantı kurulamadı..");
        });
    }
    else if (e === 'new_clicked') {
        // yeni yorum eklemek için ilgili kodları yaz
        var txt = $('#new_comment_text').val();

        $.ajax({
            method: "POST",
            url: "/Comment/Create",
            data: { "text": txt, "noteid": noteid }
        }).done(function (data) {
            if (data.result) {
                $('#modal-comment-body').load("/Comment/ShowNoteComments/" + noteid)
            } else {
                alert("Yorum eklenemedi..");
            }

        }).fail(function () {
            alert("Sunucuyla bağlantı kurulamadı..");
        });
    }
}
