import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:kogebog_flutter/models/profile.dart';
import 'package:kogebog_flutter/widgets/modern_button.dart';
import '../providers/profile_provider.dart';

class WelcomeView extends ConsumerStatefulWidget {
  const WelcomeView({super.key});

  @override
  WelcomeViewState createState() => WelcomeViewState();
}

class WelcomeViewState extends ConsumerState<WelcomeView> {
  final TextEditingController _nameController = TextEditingController();
  final _formKey = GlobalKey<FormState>();

  bool _isFormValid = false;

  @override
  Widget build(BuildContext context) {
    final profileId = ref.watch(profileProvider);

    if (profileId != null) {
      WidgetsBinding.instance.addPostFrameCallback((_) {
        context.go('/recipes');
      });
      return Scaffold(
        body: Center(child: CircularProgressIndicator()),
      );
    }

    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 45),
        child: Column(
          children: [
            const SizedBox(height: 80),
            Text(
              "Kogebogen",
              style: GoogleFonts.staatliches(fontSize: 45),
            ),
            const Spacer(),
            Align(
              alignment: Alignment.center,
              child: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  Text(
                    "Indtast dit navn",
                    style: Theme.of(context).textTheme.bodyLarge,
                  ),
                  const SizedBox(height: 10),
                  // Wrap TextField with a Form widget for validation
                  Form(
                    key: _formKey,
                    onChanged: () {
                      setState(() {
                        _isFormValid = _formKey.currentState?.validate() ?? false;
                      });
                    },
                    child: SizedBox(
                      width: 300, // Max width for input field
                      child: TextFormField(
                        controller: _nameController,
                        textAlign: TextAlign.center,
                        decoration: InputDecoration(
                          hintText: "Dit navn",
                        ),
                        validator: (value) {
                          if (value == null || value.isEmpty) {
                            return 'Dit navn må ikke være tomt';
                          } else if (value.length > 30) {
                            return 'Dit navn må ikke være længere end 30 karakterer';
                          }
                          return null;
                        },
                      ),
                    ),
                  ),
                  const SizedBox(height: 20),
                  ModernButton(
                    text: "start",
                    onPressed:
                        _isFormValid
                            ? () async {
                              Profile newProfile = Profile(
                                id: '',
                                name: _nameController.text,
                              );
                              await ref
                                  .read(profileProvider.notifier)
                                  .createProfile(newProfile);

                              if (context.mounted) {
                                context.go('/recipes');
                              }
                            }
                            : null,
                  ),
                ],
              ),
            ),
            const Spacer(flex: 2),
          ],
        ),
      ),
    );
  }
}
