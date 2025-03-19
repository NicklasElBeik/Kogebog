import 'package:kogebog_flutter/models/recipe.dart';

class Profile {
  final String id;
  final String name;
  final List<Recipe>? recipes;

  Profile({required this.id, required this.name, this.recipes});

  factory Profile.fromJson(Map<String, dynamic> json) {
  return Profile(
    id: json['id'],
    name: json['name'],
    recipes: List<Recipe>.from(json['recipes'].map((x) => Recipe.fromJson(x))),
  );
}

  Map<String, dynamic> toJson() {
    return {
      "id": id,
      "name": name,
    };
  }
}
