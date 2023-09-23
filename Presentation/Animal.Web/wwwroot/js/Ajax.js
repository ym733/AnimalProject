let divNum = 1;

function changeMain(url) {
	if (url === window.location.pathname) {
		return;
	}

	history.pushState({ url, divNum }, null, url, divNum);
	ajaxCall(url);
}

window.addEventListener('popstate', function (event) {
	if (event.state) {
		if (divNum > event.state.divNum + 1) {
			ajaxBackCall();
		} else {
			const url = event.state.url;
			ajaxCall(url);
		}
	} else {
		ajaxBackCall();
	}
});

function ajaxBackCall() {
	$(`#level${divNum}`).remove();
	divNum--;
	$(`#level${divNum}`).removeAttr(`hidden`);
}

function ajaxCall(url) {
	$.ajax({
		url: url,
		method: "GET",
		dataType: "html",
		success: (data) => {
			$(`#level${divNum}`).attr("hidden", "hidden")
			divNum++;
			$(`main`).append(`<div id="level${divNum}">${data}</div>`)
		},
		error: (err) => {
			console.error(err);
		}
	});
}
function formSubmit(url, form, event) {
	event.preventDefault();

	$.ajax({
		type: "POST",
		url: url,
		data: new FormData(form),
		contentType: false,
		processData: false,
		success: function (data) {
			$(`#level${divNum}`).attr("hidden", "hidden")
			divNum++;
			$(`main`).append(`<div id="level${divNum}">${data}</div>`)
		},
		error: function (err) {
			console.error(err);
		}
	});
}