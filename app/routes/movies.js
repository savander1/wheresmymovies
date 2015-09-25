var express = require('express');
var router = express.Router();

var movie = {
  title: "The Amazing Foo!"
}

/* GET movies listing. */
router.get('/', function(req, res, next) {
  res.json(movie);
});


module.exports = router;