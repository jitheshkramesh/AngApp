$(function () {
    $("#Leave.form").validate({
        rules: {
            LV_EMAIL: {
                required: true,
                email:true
            }
        },
        messages: {
            LV_EMAIL: {
                required: 'Please enter an Email Address.',
                email:'Please enter a <em>valid</em> email address.'
            }
        }
    });
});