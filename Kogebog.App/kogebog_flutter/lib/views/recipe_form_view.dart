import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:image_picker/image_picker.dart';
import 'package:kogebog_flutter/models/recipe.dart';
import '../providers/profile_provider.dart';
import 'dart:io';

class RecipeFormView extends ConsumerStatefulWidget {
  const RecipeFormView({super.key});

  @override
  RecipeFormViewState createState() => RecipeFormViewState();
}

class RecipeFormViewState extends ConsumerState<RecipeFormView> {
  final _titleController = TextEditingController();
  final _ingredientController = TextEditingController();
  List<String> ingredients = [];
  File? _image;

  Future<void> _pickImage() async {
    final ImagePicker picker = ImagePicker();
    final XFile? image = await picker.pickImage(source: ImageSource.camera);
    if (image != null) {
      setState(() {
        _image = File(image.path);
      });
    }
  }

  void _addIngredient() {
    if (_ingredientController.text.isNotEmpty) {
      setState(() {
        ingredients.add(_ingredientController.text);
        _ingredientController.clear();
      });
    }
  }

  Future<void> _saveRecipe() async {
    final userId = ref.read(profileProvider);
    if (userId == null) return;

    // new Recipe(id: '', title: _titleController.text, image: _image, recipeIngredients: recipeIngredients)
    // final apiService = ApiService();
    // await apiService.createRecipe(
    //   userId,
    //   _titleController.text,
    //   ingredients,
    //   _image,  // Image file to be uploaded
    // );

    Navigator.pop(context); // Go back to recipe list
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text("New Recipe")),
      body: Padding(
        padding: EdgeInsets.all(16.0),
        child: Column(
          children: [
            TextField(controller: _titleController, decoration: InputDecoration(labelText: "Recipe Title")),
            SizedBox(height: 10),
            Row(
              children: [
                Expanded(
                  child: TextField(controller: _ingredientController, decoration: InputDecoration(labelText: "Ingredient")),
                ),
                IconButton(icon: Icon(Icons.add), onPressed: _addIngredient),
              ],
            ),
            Expanded(
              child: ListView.builder(
                itemCount: ingredients.length,
                itemBuilder: (context, index) {
                  return ListTile(
                    title: Text(ingredients[index]),
                    trailing: IconButton(
                      icon: Icon(Icons.delete),
                      onPressed: () => setState(() => ingredients.removeAt(index)),
                    ),
                  );
                },
              ),
            ),
            _image != null ? Image.file(_image!, height: 100) : Container(),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                ElevatedButton(onPressed: _pickImage, child: Text("Take Picture")),
                ElevatedButton(onPressed: _saveRecipe, child: Text("Save Recipe")),
              ],
            ),
          ],
        ),
      ),
    );
  }
}
